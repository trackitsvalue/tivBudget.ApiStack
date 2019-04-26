using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Services
{
  public static class AccountService
  {
    public static Account GetAccountFromId(List<Account> accounts, Guid accountId)
    {
      if (accounts != null && accounts.Count > 0)
      {
        return accounts.FirstOrDefault((account) => account.Id == accountId);
      }

      return null;
    }

    public static AccountCategory GetCategoryOfAccountFromId(Account account, Guid accountCategoryId)
    {
      if (account != null && account.AccountCategories != null && account.AccountCategories.Count > 0)
      {
        return account.AccountCategories.FirstOrDefault((accountCategory) => accountCategory.Id == accountCategoryId);
      }

      return null;
    }

    public static List<Account> GetAllowedAccountTypes(List<Account> accounts, string allowedAccountTypes)
    {
      var accountsOfType = new List<Account>();
      if (accounts != null && accounts.Count > 0)
      {
        accountsOfType = accounts.FindAll(
          (account) => allowedAccountTypes.Contains($"|{account.AccountTypeId}|") && account.IsEnabled == true
        );
      }

      return accountsOfType;
    }

    public static Account GetDefaultOrFirstOfAccountTypes(List<Account> accounts, string allowedAccountTypes)
    {
      var accountsOfType = new List<Account>();
      if (accounts != null && accounts.Count > 0)
      {
        accountsOfType = accounts.FindAll(
          (account) => allowedAccountTypes.Contains($"|{account.AccountTypeId}|") && account.IsEnabled == true
        );
        if (accountsOfType.Count > 0)
        {
          var defaultAccount = accountsOfType.FirstOrDefault((at) => at.IsDefaultOfType);
          if (defaultAccount != null)
          {
            return defaultAccount;
          }

          return accountsOfType[0];
        }
      }

      return null;
    }

    public static AccountCategory GetDefaultOrFirstOfAccountCategories(List<AccountCategory> accountsCategories)
    {
      if (accountsCategories != null && accountsCategories.Count > 0)
      {
        var defaultAccountCategory = accountsCategories.FirstOrDefault((ac) => ac.IsDefault);
        if (defaultAccountCategory != null)
        {
          return defaultAccountCategory;
        }

        return accountsCategories[0];
      }

      return null;
    }
  }
}
