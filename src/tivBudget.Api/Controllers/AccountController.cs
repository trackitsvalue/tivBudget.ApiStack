using System;
using System.Collections.Generic;
using freebyTech.Common.ExtensionMethods;
using freebyTech.Common.Web.Logging.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Api.Services;
using tivBudget.Api.Services.Interfaces;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Account API Controller.
  /// </summary>
  [Route("accounting/accounts")]
  [ApiController]
  [Authorize]
  public class AccountController : ControllerBase
  {
    private IAccountRepository AccountRepo { get; }
    private IUserRepository UserRepo { get; }
    private IAccountService AccountService { get; }
    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountRepository">Repo to use for account information.</param>
    /// <param name="userRepository">Repo to user for user information.</param>
    /// <param name="accountService">Service for account balance information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public AccountController(IAccountRepository accountRepository, IUserRepository userRepository, IAccountService accountService, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      UserRepo = userRepository;
      AccountRepo = accountRepository;
      AccountService = accountService;
    }

    /// <summary>
    /// Returns all accounts owned by a given user with account summary data for the given month and year.
    /// </summary>
    /// <param name="month">The month the account data is relevant for.</param>
    /// <param name="year">The year the account data is relevant for.</param>
    /// <returns>A fully populated accounts array with actuals for the relevant month.</returns>
    [HttpGet("all/{year}/{month}")]
    public IActionResult Get(int year, int month)
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      // Get last day of month previous to passed month.
      var lastDayOfLastMonth = DateTimeExtensions.EndOfPreviousMonth(month, year);
      // Get last day of this month.
      var lastDayOfThisMonth = DateTimeExtensions.EndOfMonth(month, year);

      var accountTypes = AccountService.GetAllAccountsOverview(userFromAuth.Id, year, month);

      return Ok(accountTypes);
    }

    /// <summary>
    /// Returns all basic accounts owned by a given user.
    /// </summary>
    [HttpGet("all")]
    public IActionResult Get()
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var accounts = AccountRepo.FindAllByOwner(userFromAuth.Id);

      return Ok(CleanDoubleReferences(accounts));
    }

    /// <summary>
    /// Upserts a budget into the user's or owners context as long as the user has the security to affect the given upsert.
    /// </summary>
    [HttpPut()]
    public IActionResult Put([FromBody] AllAccountsOverview allAccounts)
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      // RequestLogger.UserId = userFromAuth.Id.ToString();

      // var accounts = AccountRepo.FindAllByOwner(userFromAuth.Id);
      // CompleteMissingAccountActuals(budget, userFromAuth.Id, accounts);
      // AccountRepo.Upsert(budget, userFromAuth.UserName);
      // var savedBudget = BudgetRepo.FindById(userFromAuth.Id, budget.Id);
      // savedBudget.UpgradeBudgetIfNeeded(AccountRepo.FindAllByOwner(userFromAuth.Id));
      AccountRepo.UpsertAccountChanges(allAccounts, userFromAuth.UserName);
      var accountTypes = AccountService.GetAllAccountsOverview(userFromAuth.Id, allAccounts.RelevantYear, allAccounts.RelevantMonth);
      return Ok(accountTypes);
    }

    private List<Account> CleanDoubleReferences(List<Account> accounts)
    {
      if (accounts != null && accounts.Count > 0)
      {
        foreach (var account in accounts)
        {
          account.AccountTemplate.Accounts = null;
        }
      }

      return accounts;
    }
  }
}