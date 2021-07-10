using freebyTech.Common.Web.Logging.Interfaces;
using freebyTech.Common.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Repositories.Interfaces;
using System;
using tivBudget.Api.Models;
using System.Collections.Generic;
using tivBudget.Api.Services;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Public Content API Controller.
  /// </summary>
  [Route("public")]
  [ApiController]
  public class ReportController : ControllerBase
  {
    private IReportRepository ReportRepo { get; }
    private IApiRequestLogger RequestLogger { get; }
    private IUserRepository UserRepo { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="reportRepository">Repo for report information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    /// <param name="userRepository">The user repository.</param>
    public ReportController(IReportRepository reportRepository, IApiRequestLogger requestLogger, IUserRepository userRepository)
    {
      RequestLogger = requestLogger;
      ReportRepo = reportRepository;
      UserRepo = userRepository;
    }

    /// <summary>
    /// Returns all the published reports.
    /// </summary>
    /// <returns>A list of published reports.</returns>
    [HttpGet("reports")]
    public IActionResult GetAllReports()
    {
      var reports = ReportRepo.FindAllReports();

      return Ok(reports);
    }

    /// <summary>
    /// Returns all the published reports.
    /// </summary>
    /// <returns>A list of published reports.</returns>
    [HttpGet("reports/budget-dashboard")]
    public IActionResult GetBudgetDashboardReports() //[FromBody] BudgetDashboardRequest dashboardCopyRequest)
    {
      var dashboardCopyRequest = new BudgetDashboardRequest() { StartDate = "5/1/2021", EndDate = "6/31/2021" };
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var reports = ReportRepo.FindAllReports();

      return Ok(reports);
    }
  }
}