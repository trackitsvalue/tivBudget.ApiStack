using freebyTech.Common.Web.Logging.Interfaces;
using freebyTech.Common.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Repositories.Interfaces;
using System;
using tivBudget.Api.Models;
using System.Collections.Generic;

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

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="reportRepository">Repo for report information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public ReportController(IReportRepository reportRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      ReportRepo = reportRepository;
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
  }
}