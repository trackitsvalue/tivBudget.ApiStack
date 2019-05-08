using System;
using System.Collections.Generic;
using System.Linq;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Web.Logging.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
  public class BudgetController : ControllerBase
  {
    private IBudgetRepository BudgetRepo { get; }
    private IAccountRepository AccountRepo { get; }
    private IAccountTemplateRepository AccountTemplateRepo { get; }

    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="budgetRepository">Repo to use for budget information.</param>
    /// <param name="accountTemplateRepository">Repo to use for account template information.</param>
    public BudgetController(IBudgetRepository budgetRepository, IAccountTemplateRepository accountTemplateRepository, IAccountRepository accountRepository, IApiRequestLogger requestLogger)
    {
      BudgetRepo = budgetRepository;
      AccountTemplateRepo = accountTemplateRepository;
      AccountRepo = accountRepository;
      RequestLogger = requestLogger;
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
      // Me
      // var userId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var userId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");
      RequestLogger.UserId = userId.ToString();

      var budget = BudgetRepo.FindByIndex(userId, description, month, year);

      // Not found, create a new "blank" budget they can use instead.
      if (budget == null)
      {
        budget = BudgetService.BuildNewBudget(description, year, month, userId, "James");
      }
      budget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userId));
      return Ok(budget);
    }

    /// <summary>
    /// Upserts a budget into the user's or owners context as long as the user has the security to affect the given upsert.
    /// </summary>
    [HttpPut()]
    public IActionResult Put([FromBody] Budget budget)
    {
      // Me
      // var userId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var userId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");
      RequestLogger.UserId = userId.ToString();

      var accounts = AccountRepo.FindAllByOwner(userId);
      CompleteMissingAccountActuals(budget, userId, accounts);
      BudgetRepo.Upsert(budget, "James");
      var savedBudget = BudgetRepo.FindById(userId, budget.Id);
      savedBudget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userId));
      return Ok(savedBudget);
    }

    /// <summary>
    /// Deletes a budget from the user's or owners context as long as the user has the security to delete the given budget and returns a blank budget.
    /// </summary>
    [HttpDelete()]
    public IActionResult Delete([FromBody] Budget budget)
    {
      // Me
      // var userId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var userId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");
      RequestLogger.UserId = userId.ToString();

      if (!budget.IsNew)
      {
        BudgetRepo.Delete(userId, budget.Id);
      }
      var blankBudget = BudgetService.BuildNewBudget(budget.Description, budget.Year, budget.Month, userId, "James");
      blankBudget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userId));
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

    private AccountActualTemplate GetDefaultActualTemplateForAccountOfIncomeType(List<AccountTemplate> accountTemplates, Guid accountTemplateId, bool isIncomeCategory)
    {
      AccountActualTemplate accountActualTemplate = null;

      var accountTemplate = accountTemplates.FirstOrDefault(m => m.Id.CompareTo(accountTemplateId) == 0);
      if (accountTemplate != null)
      {
        // Pull default actual template and use that to build the account actual.
        if (isIncomeCategory)
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

    private bool GetAccountObjectsNeededForLinking(List<Account> ownerAccounts, List<AccountTemplate> accountTemplates, bool isIncomeCategory, Guid accountLinkId, Guid? accountCategoryLinkId,
      out AccountCategory accountCategory, out AccountActualTemplate accountActualTemplate)
    {
      accountCategory = null;
      accountActualTemplate = null;

      if (GetAccountAndCategoryFromLinkIDs(ownerAccounts, accountLinkId, accountCategoryLinkId, out Account account, out accountCategory))
      {
        accountActualTemplate = GetDefaultActualTemplateForAccountOfIncomeType(accountTemplates, account.AccountTemplate.Id, isIncomeCategory);
        if (accountActualTemplate != null) return true;
      }
      return false;
    }

    private void CompleteMissingAccountActuals(Budget budget, Guid ownerId, List<Account> ownerAccounts)
    {
      List<AccountTemplate> accountTemplates = null;
      AccountCategory budgetAccountCategory = null;
      AccountActualTemplate budgetIncomeActualTemplate = null;
      AccountActualTemplate budgetExpenseActualTemplate = null;

      if (budget.AccountLinkId.HasValue)
      {
        if (accountTemplates == null)
        {
          accountTemplates = AccountTemplateRepo.FindAllTemplatesByOwner(ownerId);
        }

        if (GetAccountAndCategoryFromLinkIDs(ownerAccounts, budget.AccountLinkId.Value, budget.AccountCategoryLinkId, out Account budgetAccount, out budgetAccountCategory))
        {
          budgetIncomeActualTemplate = GetDefaultActualTemplateForAccountOfIncomeType(accountTemplates, budgetAccount.AccountTemplateId, true);
          budgetExpenseActualTemplate = GetDefaultActualTemplateForAccountOfIncomeType(accountTemplates, budgetAccount.AccountTemplateId, false);
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
                if (GetAccountObjectsNeededForLinking(ownerAccounts, accountTemplates,
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
            // --------------------------------------------------------------------------------------------
            if (budgetIncomeActualTemplate != null && budgetExpenseActualTemplate != null)
            {
              var defaultAABudgetLink = budgetActual.AccountActuals.FirstOrDefault(aa => aa.IsBudgetDefaultLink);
              if(budgetActual.IsNew || (defaultAABudgetLink == null)) {
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
                    ? budgetIncomeActualTemplate.Id
                    : budgetExpenseActualTemplate.Id,
                  RelevantOn = budgetActual.RelevantOn,
                  IsBudgetDefaultLink = true,
                };
                budgetActual.AccountActuals.Add(linkedAccountActual);
              }
              else if(budgetActual.IsDirty)
              {
                // Change anything about link that can change when actual is modified.
                defaultAABudgetLink.CategoryId = budgetAccountCategory.Id;
                defaultAABudgetLink.IsDirty = true;
                defaultAABudgetLink.Description =
                  $"{budgetActual.Description} on budget from {budget.Month}/{budget.Year}".VerifySize(256);
                defaultAABudgetLink.Amount = budgetActual.Amount;
                defaultAABudgetLink.ActualTemplateId = budgetCategory.CategoryTemplate.IsIncomeCategory
                  ? budgetIncomeActualTemplate.Id
                  : budgetExpenseActualTemplate.Id;
                defaultAABudgetLink.RelevantOn = budgetActual.RelevantOn;
              }
              else if (budgetActual.IsDeleted)
              {
                foreach (var actual in budgetActual.AccountActuals)
                {
                  actual.IsDeleted = true;
                }
              }
            }
          }
        }
      }
    }

    #endregion
  }
}