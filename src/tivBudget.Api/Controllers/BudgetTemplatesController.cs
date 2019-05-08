using System;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Budget Template API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class BudgetTemplatesController : ControllerBase
  {
    private IBudgetCategoryTemplateRepository BudgetCategoryTemplateRepo { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="budgetCategoryTemplateRepository">Repo to use for budget category template information.</param>
    public BudgetTemplatesController(IBudgetCategoryTemplateRepository budgetCategoryTemplateRepository)
    {
      BudgetCategoryTemplateRepo = budgetCategoryTemplateRepository;
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

      var budgetCategoryTemplates = BudgetCategoryTemplateRepo.FindAllTemplatesByOwner(ownerId);

      return Ok(budgetCategoryTemplates);
    }
  }
}