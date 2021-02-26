using System;
using freebyTech.Common.Web.Logging.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Api.Services;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Account Template API Controller.
  /// </summary>
  [Route("accounting/templates")]
  [ApiController]
  [Authorize]
  public class AccountTemplatesController : ControllerBase
  {
    private IAccountTemplateRepository AccountTemplateRepo { get; }
    private IUserRepository UserRepo { get; }

    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountTemplateRepository"></param>
    /// <param name="userRepository">Repo to user for user information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public AccountTemplatesController(IAccountTemplateRepository accountTemplateRepository, IUserRepository userRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      UserRepo = userRepository;
      AccountTemplateRepo = accountTemplateRepository;
    }

    /// <summary>
    /// Returns all unowned and owned budget categories for a particular ownerId.
    /// </summary>
    /// <returns>A list of all unowned and owned budget categories for a particular ownerId.</returns>
    [HttpGet()]
    public IActionResult Get()
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var accountTemplates = AccountTemplateRepo.FindAllTemplatesByOwner(userFromAuth.Id);

      return Ok(accountTemplates);
    }
  }
}