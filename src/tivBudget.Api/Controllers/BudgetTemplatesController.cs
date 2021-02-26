using System;
using freebyTech.Common.Web.Logging.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Api.Services;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Budget Template API Controller.
  /// </summary>
  [Route("budgeting/templates")]
  [ApiController]
  [Authorize]
  public class BudgetTemplatesController : ControllerBase
  {
    private IBudgetCategoryTemplateRepository BudgetCategoryTemplateRepo { get; }
    private IUserRepository UserRepo { get; }

    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="budgetCategoryTemplateRepository">Repo to use for budget category template information.</param>
    /// <param name="userRepository">Repo to user for user information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public BudgetTemplatesController(IBudgetCategoryTemplateRepository budgetCategoryTemplateRepository, IUserRepository userRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      UserRepo = userRepository;
      BudgetCategoryTemplateRepo = budgetCategoryTemplateRepository;
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

      var budgetCategoryTemplates = BudgetCategoryTemplateRepo.FindAllTemplatesByOwner(userFromAuth.Id);

      return Ok(budgetCategoryTemplates);
    }
  }
}