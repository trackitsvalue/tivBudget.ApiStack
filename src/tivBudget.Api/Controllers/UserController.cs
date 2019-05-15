using System;
using System.Collections.Generic;
using freebyTech.Common.Web.Logging.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Api.Models;
using tivBudget.Api.Services;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using tivBudget.Dal.VirtualModels;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Account API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  [Authorize]
  public class UserController : ControllerBase
  {
    private IBudgetRepository BudgetRepo { get; }
    private IAccountRepository AccountRepo { get; }
    private IUserRepository UserRepo { get; }

    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountRepository">Repo to use for account information.</param>
    /// <param name="budgetRepository">Repo to use for budget information.</param>
    /// <param name="userRepository">Repo to user for user information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public UserController(IAccountRepository accountRepository, IBudgetRepository budgetRepository, IUserRepository userRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      UserRepo = userRepository;
      AccountRepo = accountRepository;
      BudgetRepo = budgetRepository;
    }

    /// <summary>
    /// Returns teh status of the user.
    /// </summary>
    [HttpGet("status")]
    public IActionResult Get()
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var statusModel = new UserStatusModel()
      {
        IsEnabled = userFromAuth.IsEnabled,
        AccountCount = AccountRepo.FindCountByOwner(userFromAuth.Id),
        BudgetCount = BudgetRepo.FindCountByOwner(userFromAuth.Id),
      };
      if (statusModel.AccountCount == 0 || statusModel.BudgetCount == 0)
      {
        statusModel.IsNew = true;
      }
      return Ok(statusModel);
    }
  }
}