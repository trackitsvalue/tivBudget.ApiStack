using System;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Budget API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class AccountTemplatesController : ControllerBase
  {
    private IAccountTemplateRepository AccountTemplateRepo { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountCategoryTemplateRepository">Repo to use for account category template information.</param>
    public AccountTemplatesController(IAccountTemplateRepository accountTemplateRepository)
    {
      AccountTemplateRepo = accountTemplateRepository;
    }

    /// <summary>
    /// Returns all unowned and owned budget categories for a particular ownerId.
    /// </summary>
    /// <returns>A list of all unowned and owned budget categories for a particular ownerId.</returns>
    [HttpGet()]
    public IActionResult Get()
    {
      // Me
      // var ownerId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var ownerId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");

      var accountTemplates = AccountTemplateRepo.FindAllTemplatesByOwner(ownerId);

      return Ok(accountTemplates);
    }
  }
}