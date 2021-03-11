using System;
using System.Collections.Generic;
using System.Linq;
using tivBudget.Dal.Models;
using freebyTech.Common.ExtensionMethods;
using tivBudget.Api.Services.Interfaces;
using freebyTech.Common.Web.Logging.Interfaces;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Services
{
  public static class AccountExtensionFunctions
  {
    public static BalanceInfo ToBalanceInfo(this DateTime date)
    {
      return new BalanceInfo() { Month = date.Month, Day = date.Day, Year = date.Year };
    }
  }

  /// <summary>
  /// Service class that simplifies standard user operations.
  /// </summary>
  public class AccountService : IAccountService
  {
    private IAccountTemplateRepository AccountTemplateRepo { get; }
    private IAccountRepository AccountRepo { get; }
    private IAccountBalanceRepository AccountBalanceRepo { get; }
    private IUserRepository UserRepo { get; }
    private IAccountTypeRepository AccountTypeRepository { get; }
    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountRepository">Repo to use for account information.</param>
    /// <param name="accountBalanceRepository">Repo for account balance information.</param>
    /// <param name="accountTemplateRepository">Repo fro account template information.</param>
    /// <param name="accountTypeRepository">Repo for account type information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public AccountService(IAccountRepository accountRepository, IAccountBalanceRepository accountBalanceRepository, IAccountTemplateRepository accountTemplateRepository, IAccountTypeRepository accountTypeRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      AccountRepo = accountRepository;
      AccountBalanceRepo = accountBalanceRepository;
      AccountTemplateRepo = accountTemplateRepository;
      AccountTypeRepository = accountTypeRepository;
    }

    public AllAccountsOverview GetAllAccountsOverview(Guid ownerId, int year, int month)
    {
      var accountsResponse = new AllAccountsOverview
      {
        RelevantMonth = month,
        RelevantYear = year
      };

      // Get last day of month previous to passed month.
      var lastDayOfLastMonth = DateTimeExtensions.EndOfPreviousMonth(month, year);

      // Get last day of this month.
      var lastDayOfThisMonth = DateTimeExtensions.EndOfMonth(month, year);

      accountsResponse.StartOfMonth = lastDayOfLastMonth.ToBalanceInfo();
      accountsResponse.EndOfMonth = lastDayOfThisMonth.ToBalanceInfo();

      var accountTypesCollection = AccountTypeRepository.GetAllAccountTypes();
      var accountsCollection = AccountRepo.FindAllByOwnerAndMonth(ownerId, year, month);
      var accountCategoryValuesLastMonth = AccountBalanceRepo.GetAllAccountBalances(ownerId, lastDayOfLastMonth);
      var accountCategoryValuesEndOfMonth = AccountBalanceRepo.GetAllAccountBalances(ownerId, lastDayOfThisMonth);

      var allAccountsFromAllTypes = new List<AccountsOfTypeOverview>();
      foreach (var accountType in accountTypesCollection.OrderBy(m => m.Id))
      {
        var relevantAccountEntities = accountsCollection.Where(m => m.AccountTypeId == accountType.Id);
        if (relevantAccountEntities.Count() > 0)
        {
          var allAccountsFromType = new List<AccountOverview>();
          var accountTypeInfo = new AccountsOfTypeOverview()
          {
            Info = accountType,
            AreAccountsOpen = false,
            StartOfMonth = lastDayOfLastMonth.ToBalanceInfo(),
            EndOfMonth = lastDayOfThisMonth.ToBalanceInfo()
          };
          allAccountsFromAllTypes.Add(accountTypeInfo);
          foreach (var accountEntity in relevantAccountEntities.OrderBy(m => m.DisplayIndex))
          {
            allAccountsFromType.Add(accountEntity);

            foreach (var accountCategory in accountEntity.AccountCategories)
            {
              var lastMonthBalance = accountCategoryValuesLastMonth.FirstOrDefault(m => m.Id.CompareTo(accountCategory.Id) == 0);
              if (lastMonthBalance != null)
              {
                accountCategory.StartingBalance = lastMonthBalance.CurrentBalance;
                accountEntity.StartingBalance += lastMonthBalance.CurrentBalance;
                accountTypeInfo.StartOfMonth.Balance += lastMonthBalance.CurrentBalance;
                accountsResponse.StartOfMonth.Balance += lastMonthBalance.CurrentBalance;
              }
              var endOfMonthBalance = accountCategoryValuesEndOfMonth.FirstOrDefault(m => m.Id.CompareTo(accountCategory.Id) == 0);
              if (endOfMonthBalance != null)
              {
                accountCategory.EndingBalance = endOfMonthBalance.CurrentBalance;
                accountEntity.EndingBalance += endOfMonthBalance.CurrentBalance;
                accountTypeInfo.EndOfMonth.Balance += endOfMonthBalance.CurrentBalance;
                accountsResponse.EndOfMonth.Balance += endOfMonthBalance.CurrentBalance;
              }
              accountCategory.Delta = accountCategory.EndingBalance - accountCategory.StartingBalance;
            }
            accountEntity.Delta = accountEntity.EndingBalance - accountEntity.StartingBalance;
          }
          accountTypeInfo.Delta = accountTypeInfo.EndOfMonth.Balance - accountTypeInfo.StartOfMonth.Balance;
          accountTypeInfo.Accounts = allAccountsFromType.ToArray();
        }
        
      }
      accountsResponse.Delta = accountsResponse.EndOfMonth.Balance - accountsResponse.StartOfMonth.Balance;
      accountsResponse.AccountTypes = allAccountsFromAllTypes.ToArray();

      return accountsResponse;
    }
  }
}
