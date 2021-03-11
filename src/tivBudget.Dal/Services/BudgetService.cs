using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tivBudget.Dal.Constants;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Services
{
  public static class BudgetService
  {
    public static Budget BuildNewBudget(string description, int year, int month, Guid userId, string userName)
    {
      return new Budget
      {
        CreatedBy = userName, CreatedOn = DateTime.Now, Description = description, Id = Guid.NewGuid(),
        OwnerId = userId, IsNew = true, IsDirty = true, StartDate = new DateTime(year, month, 1), Year = year, Month = month,
        BudgetCategories = new List<BudgetCategory>(),
      };
    }

    public static void UpgradeBudgetIfNeeded(this Budget budget, List<Account> accounts)
    {
      // Budget level default account linkage is a new feature.
      if (!budget.AccountLinkId.HasValue || !budget.AccountCategoryLinkId.HasValue)
      {
        Account selectedtAccount = null;

        // Try to give an intelligent default with graceful failure for defaulting account links, this is not required but much of the new
        // applications functionality is based upon the new default account linking.
        if (accounts.Count > 0)
        {
          if (!budget.AccountLinkId.HasValue)
          {
            selectedtAccount =
              AccountService.GetDefaultOrFirstOfAccountTypes(accounts, $"|{AccountTypeEnum.BankAccount:D}|");
            if (selectedtAccount == null)
            {
              selectedtAccount =
                AccountService.GetDefaultOrFirstOfAccountTypes(accounts, $"|{AccountTypeEnum.CashAccount:D}");
            }

            if (selectedtAccount != null)
            {
              budget.AccountLinkId = selectedtAccount.Id;
            }
          }
          else
          {
            selectedtAccount = AccountService.GetAccountFromId(accounts, budget.AccountLinkId.Value);
          }
        }

        if (selectedtAccount != null)
        {
          var selectedCategory =
            AccountService.GetDefaultOrFirstOfAccountCategories(selectedtAccount.AccountCategories.ToList());
          if (selectedCategory != null)
          {
            budget.AccountCategoryLinkId = selectedCategory.Id;
          }
        }
      }
    }
  }
}
