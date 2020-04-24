using System;
using System.Collections.Generic;
using System.Linq;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Web.Logging.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tivBudget.Api.Services;
using tivBudget.Dal.Constants;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using tivBudget.Dal.Services;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Budget API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  [Authorize]
  public class BudgetController : ControllerBase
  {
    private IBudgetRepository BudgetRepo { get; }
    private IAccountRepository AccountRepo { get; }
    private IAccountTemplateRepository AccountTemplateRepo { get; }
    private IUserRepository UserRepo { get; }

    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="budgetRepository">Repo to use for budget information.</param>
    /// <param name="accountTemplateRepository">Repo to use for account template information.</param>
    /// <param name="accountRepository">Repo to use for account information.</param>
    /// <param name="userRepository">Repo to user for user information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public BudgetController(IBudgetRepository budgetRepository, IAccountTemplateRepository accountTemplateRepository, IAccountRepository accountRepository, IUserRepository userRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      UserRepo = userRepository;
      BudgetRepo = budgetRepository;
      AccountTemplateRepo = accountTemplateRepository;
      AccountRepo = accountRepository;
    }
    /// <summary>
    /// Returns a fully populated budget of the given name of the given month and year if it exists and the user owns or has access to that budget. If
    /// the budget doesn't already exist then it returns a blank budget.
    /// </summary>
    /// <param name="description">The description of the budget, for a user's primary budgets the description is always "default".</param>
    /// <param name="month">The month the budget is relevant for.</param>
    /// <param name="year">The year the budget is relevant for.</param>
    /// <returns>A fully populated budget object.</returns>
    [HttpGet("{description}/{year}/{month}")]
    public IActionResult Get(string description, int year, int month)
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var budget = BudgetRepo.FindByIndex(userFromAuth.Id, description, month, year);

      // Not found, create a new "blank" budget they can use instead.
      if (budget == null)
      {
        budget = BudgetService.BuildNewBudget(description, year, month, userFromAuth.Id, userFromAuth.UserName);
      }
      budget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userFromAuth.Id));
      return Ok(budget);
    }

    /// <summary>
    /// Upserts a budget into the user's or owners context as long as the user has the security to affect the given upsert.
    /// </summary>
    [HttpPut()]
    public IActionResult Put([FromBody] Budget budget)
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var accounts = AccountRepo.FindAllByOwner(userFromAuth.Id);
      CompleteMissingAccountActuals(budget, userFromAuth.Id, accounts);
      BudgetRepo.Upsert(budget, userFromAuth.UserName);
      var savedBudget = BudgetRepo.FindById(userFromAuth.Id, budget.Id);
      savedBudget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userFromAuth.Id));
      return Ok(savedBudget);
    }

    /// <summary>
    /// Deletes a budget from the user's or owners context as long as the user has the security to delete the given budget and returns a blank budget.
    /// </summary>
    [HttpDelete()]
    public IActionResult Delete([FromBody] Budget budget)
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      if (!budget.IsNew)
      {
        BudgetRepo.Delete(userFromAuth.Id, budget.Id);
      }
      var blankBudget = BudgetService.BuildNewBudget(budget.Description, budget.Year, budget.Month, userFromAuth.Id, userFromAuth.UserName);
      blankBudget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userFromAuth.Id));
      return Ok(blankBudget);
    }

    #region Private Helper Methods

    private bool GetAccountAndCategoryFromLinkIDs(List<Account> ownerAccounts, Guid accountLinkId, Guid? accountCategoryLinkId, out Account account,
      out AccountCategory accountCategory)
    {
      account = ownerAccounts.FirstOrDefault(a => a.Id.CompareTo(accountLinkId) == 0);
      accountCategory = null;
      if (account != null && account.AccountCategories.Count > 0)
      {
        // Get specifically linked category.
        if (accountCategoryLinkId.HasValue)
        {
          accountCategory = account.AccountCategories.FirstOrDefault(ac => ac.Id.CompareTo(accountCategoryLinkId) == 0);
        }

        // Get default category if not specifically linked.
        if (accountCategory == null)
        {
          accountCategory = account.AccountCategories.FirstOrDefault(ac => ac.IsDefault);
        }

        // Get first category if not specifically the default category.
        if (accountCategory == null)
        {
          accountCategory = account.AccountCategories.ToList()[0];
        }

        return true;
      }

      return false;
    }

    private AccountActualTemplate GetDefaultActualTemplateForAccountOfIncomeType(List<AccountTemplate> accountTemplates, Guid accountTemplateId, bool isDepositTemplate)
    {
      AccountActualTemplate accountActualTemplate = null;

      var accountTemplate = accountTemplates.FirstOrDefault(m => m.Id.CompareTo(accountTemplateId) == 0);
      if (accountTemplate != null)
      {
        // Pull default actual template and use that to build the account actual.
        if (isDepositTemplate)
        {
          accountActualTemplate = accountTemplate.AccountActualTemplates.FirstOrDefault(m => m.IsDefault && m.IsDeposit);
          if (accountActualTemplate == null)
          {
            accountActualTemplate = accountTemplate.AccountActualTemplates.FirstOrDefault(m => m.IsDeposit);
          }
        }
        else
        {
          accountActualTemplate = accountTemplate.AccountActualTemplates.FirstOrDefault(m => m.IsDefault && !m.IsDeposit);
          if (accountActualTemplate == null)
          {
            accountActualTemplate = accountTemplate.AccountActualTemplates.FirstOrDefault(m => !m.IsDeposit);
          }
        }
      }
      return accountActualTemplate;
    }

    private bool GetAccountObjectsNeededForLinking(List<Account> ownerAccounts, List<AccountTemplate> accountTemplates, bool isCreditWithdrawl, bool isIncomeCategory, Guid accountLinkId, Guid? accountCategoryLinkId,
      out AccountCategory accountCategory, out AccountActualTemplate accountActualTemplate)
    {
      accountCategory = null;
      accountActualTemplate = null;

      if (GetAccountAndCategoryFromLinkIDs(ownerAccounts, accountLinkId, accountCategoryLinkId, out Account account, out accountCategory))
      {
        // When an Account links is not a credit withdrawl...
        // Account links, at the least non-"account default link", should be opposite of what the category is.
        // 1) A manual link to an account in an income category means that that item was withdrawn for the budget. 
        // 2) A manual link to an account in an expense category means that the item was deposited into the account from the budget.
        accountActualTemplate = GetDefaultActualTemplateForAccountOfIncomeType(accountTemplates, account.AccountTemplate.Id, isCreditWithdrawl ? false : !isIncomeCategory);
        if (accountActualTemplate != null) return true;
      }
      return false;
    }

    private void CompleteMissingAccountActuals(Budget budget, Guid ownerId, List<Account> ownerAccounts)
    {
      List<AccountTemplate> accountTemplates = null;
      AccountCategory budgetAccountCategory = null;
      AccountActualTemplate budgetLinkedAccountDepositActualTemplate = null;
      AccountActualTemplate budgetLinkedAccountWithdrawlActualTemplate = null;

      if (budget.AccountLinkId.HasValue)
      {
        if (accountTemplates == null)
        {
          accountTemplates = AccountTemplateRepo.FindAllTemplatesByOwner(ownerId);
        }

        if (GetAccountAndCategoryFromLinkIDs(ownerAccounts, budget.AccountLinkId.Value, budget.AccountCategoryLinkId, out Account budgetAccount, out budgetAccountCategory))
        {
          budgetLinkedAccountDepositActualTemplate = GetDefaultActualTemplateForAccountOfIncomeType(accountTemplates, budgetAccount.AccountTemplateId, true);
          budgetLinkedAccountWithdrawlActualTemplate = GetDefaultActualTemplateForAccountOfIncomeType(accountTemplates, budgetAccount.AccountTemplateId, false);
        }
      }

      foreach (var budgetCategory in budget.BudgetCategories)
      {
        foreach (var budgetItem in budgetCategory.BudgetItems)
        {
          foreach (var budgetActual in budgetItem.BudgetActuals)
          {
            if (budgetActual.AccountActuals == null)
            {
              budgetActual.AccountActuals = new List<AccountActual>();
            }

            // Build out transfer links and the like.
            // --------------------------------------
            if (budgetActual.IsNew)
            {
              if (budgetActual.IsLinked && budgetActual.AccountLinkId.HasValue)
              {
                if (accountTemplates == null)
                {
                  accountTemplates = AccountTemplateRepo.FindAllTemplatesByOwner(ownerId);
                }

                // If we found all the linking pieces then link this actual, if we didn't then delete this actual as otherwise it could cause a failure of the entire budget 
                // to save.
                if (GetAccountObjectsNeededForLinking(ownerAccounts, accountTemplates, budgetActual.IsCreditWithdrawl,
                      budgetCategory.CategoryTemplate.IsIncomeCategory, budgetActual.AccountLinkId.Value,
                      budgetActual.AccountCategoryLinkId.Value, out AccountCategory ownerAccountCategory,
                      out AccountActualTemplate accountActualTemplate) && budgetActual.AccountActuals.Count == 1)
                {
                  var linkedAccountActual = budgetActual.AccountActuals.ToArray()[0];
                  linkedAccountActual.Description = $"{budgetActual.Description} on budget from {budget.Month}/{budget.Year}".VerifySize(256);
                  linkedAccountActual.Amount = budgetActual.Amount;
                  linkedAccountActual.CategoryId = ownerAccountCategory.Id;
                  linkedAccountActual.ActualTemplateId = accountActualTemplate.Id;
                  linkedAccountActual.RelevantOn = budgetActual.RelevantOn;
                }
                else
                {
                  var accountActualTemplateId =
                    accountActualTemplate == null ? "null" : accountActualTemplate.Id.ToString();
                  RequestLogger.LogWarn(
                    "Unable to create linked Account Actual from Linked Budget Actual. Deleting stub Link.",
                    $"BudgetActual={budgetActual.Id}, Account={budgetActual.AccountLinkId}, AccountCategory={budgetActual.AccountCategoryLinkId}, AccountActualTemplate={accountActualTemplateId}");
                  budgetActual.AccountActuals.Clear();
                  budgetActual.AccountLinkId = null;
                  budgetActual.AccountCategoryLinkId = null;
                  budgetActual.IsLinked = false;
                }
              }
              else if (budgetActual.IsLinked)
              {
                var accountLinkId = budgetActual.AccountLinkId.HasValue
                  ? budgetActual.AccountLinkId.Value.ToString()
                  : "null";
                var accountCategoryLinkId = budgetActual.AccountCategoryLinkId.HasValue
                  ? budgetActual.AccountCategoryLinkId.Value.ToString()
                  : "null";
                RequestLogger.LogWarn(
                  "New Linked Budget Actual not properly linked to Account Actual. Deleting stub Link.",
                  $"BudgetActual={budgetActual.Id}, Account={accountLinkId}, AccountCategory={accountCategoryLinkId}");
                budgetActual.AccountActuals.Clear();
                budgetActual.AccountLinkId = null;
                budgetActual.AccountCategoryLinkId = null;
                budgetActual.IsLinked = false;
              }
            }

            // Build out main account links if not already built and if the budget is linked to an account.
            // Don't add if this is a credit withdrawl as that is not linked to the main account and is
            // linked only to the credit account the user setup. The payoff(s) from the virtual category
            // item(s) is what will be linked to this account.
            // --------------------------------------------------------------------------------------------
            if (budgetLinkedAccountDepositActualTemplate != null && budgetLinkedAccountWithdrawlActualTemplate != null)
            {
              var defaultAABudgetLink = budgetActual.AccountActuals.FirstOrDefault(aa => aa.IsBudgetDefaultLink);
              if (budgetActual.IsDeleted)
              {
                foreach (var actual in budgetActual.AccountActuals)
                {
                  actual.IsDeleted = true;
                }
              }
              else if (budgetActual.IsNew || defaultAABudgetLink == null)
              {
                // Only add the account link when this isn't a credit withdrawl that isn't linked to the main account.
                if (!budgetActual.IsCreditWithdrawl)
                {
                  var linkedAccountActual = new AccountActual()
                  {
                    Id = Guid.NewGuid(),
                    CategoryId = budgetAccountCategory.Id,
                    BudgetActualLinkId = budgetActual.Id,
                    IsLinked = true,
                    IsNew = true,
                    IsDirty = true,
                    Description =
                      $"{budgetActual.Description} on budget from {budget.Month}/{budget.Year}".VerifySize(256),
                    Amount = budgetActual.Amount,
                    ActualTemplateId = budgetCategory.CategoryTemplate.IsIncomeCategory
                      ? budgetLinkedAccountDepositActualTemplate.Id
                      : budgetLinkedAccountWithdrawlActualTemplate.Id,
                    RelevantOn = budgetActual.RelevantOn,
                    IsBudgetDefaultLink = true,
                  };
                  budgetActual.AccountActuals.Add(linkedAccountActual);
                }
              }
              else if (budgetActual.IsDirty)
              {
                // Change anything about link that can change when actual is modified.
                defaultAABudgetLink.CategoryId = budgetAccountCategory.Id;
                defaultAABudgetLink.IsDirty = true;
                defaultAABudgetLink.Description =
                  $"{budgetActual.Description} on budget from {budget.Month}/{budget.Year}".VerifySize(256);
                defaultAABudgetLink.Amount = budgetActual.Amount;
                defaultAABudgetLink.ActualTemplateId = budgetCategory.CategoryTemplate.IsIncomeCategory
                  ? budgetLinkedAccountDepositActualTemplate.Id
                  : budgetLinkedAccountWithdrawlActualTemplate.Id;
                defaultAABudgetLink.RelevantOn = budgetActual.RelevantOn;
              }
            }
          }
        }
      }
    }

    #endregion
  }
}