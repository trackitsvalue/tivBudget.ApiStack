using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
    
    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="budgetRepository">Repo to use for budget information.</param>
    /// <param name="accountTemplateRepository">Repo to use for account template information.</param>
    public BudgetController(IBudgetRepository budgetRepository, IAccountTemplateRepository accountTemplateRepository, IAccountRepository accountRepository)
    {
      BudgetRepo = budgetRepository;
      AccountTemplateRepo = accountTemplateRepository;
      AccountRepo = accountRepository;
    }
    /// <summary>
    /// Returns a fully populated budget of the given name of the given month and year if it exists and the user owns or has access to that budget.
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

      BudgetRepo.Upsert(budget, "James");
      return Ok("Inserted");
    }

    //private void CompleteMissingAccountActuals(Budget budget, Guid ownerId)
    //{
    //  List<AccountTemplate> accountTemplates = null;

    //  foreach (var budgetCategory in budget.BudgetCategories)
    //  {
    //    foreach (var budgetItem in budgetCategory.BudgetItems)
    //    {
    //      foreach (var budgetActual in budgetItem.BudgetActuals)
    //      {
    //        if (budgetActual.IsNew && budgetActual.AccountLink != null)
    //        {
    //          if (accountTemplates == null)
    //          {
    //            accountTemplates = AccountTemplateRepo.FindAllTemplatesByOwner(ownerId);
    //          }
    //          var accountTemplate = accountTemplates.FirstOrDefault(m => m.Id.CompareTo(budgetItem.AccountLink.AccountTemplateId) == 0);
    //          AccountActualTemplateEntity accountActualTemplate = null;
    //          if (accountTemplate != null)
    //          {
    //            // Pull default actual which is opposite of what budget category is (so if it is income on the account it should be a withdrawl and vice versa).
    //            if (budgetCategory.CategoryTemplate.IsIncomeCategory)
    //            {
    //              accountActualTemplate = accountTemplate.ActualTemplates.FirstOrDefault(m => m.IsDefault && !m.IsDeposit);
    //            }
    //            else
    //            {
    //              accountActualTemplate = accountTemplate.ActualTemplates.FirstOrDefault(m => m.IsDefault && m.IsDeposit);
    //            }
    //          }

    //        }
    //      }

    //      if (budgetItem.IsLinked)
    //      {
            

            
    //        AccountActualTemplateEntity accountActualTemplate = null;
    //        if (accountTemplate != null)
    //        {
    //          // Pull default actual which is opposite of what budget category is (so if it is income on the account it should be a withdrawl and vice versa).
    //          if (budgetCategory.CategoryTemplate.IsIncomeCategory)
    //          {
    //            accountActualTemplate = accountTemplate.ActualTemplates.FirstOrDefault(m => m.IsDefault && !m.IsDeposit);
    //          }
    //          else
    //          {
    //            accountActualTemplate = accountTemplate.ActualTemplates.FirstOrDefault(m => m.IsDefault && m.IsDeposit);
    //          }
    //        }

    //        foreach (var budgetActual in budgetItem.BudgetActuals)
    //        {
    //          if (budgetActual.LinkedAccountActuals != null)
    //          {
    //            foreach (var linkedAccountActual in budgetActual.LinkedAccountActuals)
    //            {
    //              linkedAccountActual.Description = string.Format("{0} on budget from {1}/{2}", budgetActual.Description, budget.Month, budget.Year).MaxSize(50);
    //              linkedAccountActual.Amount = budgetActual.Amount;
    //              linkedAccountActual.RelevantOn = budgetActual.RelevantOn;
    //              // If item has not been linked up before then link it up.
    //              if ((accountActualTemplate != null) && (linkedAccountActual.ActualTemplateId.CompareTo(Guid.Empty) == 0))
    //              {
    //                linkedAccountActual.ActualTemplateId = accountActualTemplate.Id;
    //              }
    //            }
    //          }
    //        }
    //      }
    //    }
    //  }
    // }
  }
}