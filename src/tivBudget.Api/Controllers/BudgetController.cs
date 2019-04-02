using System;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Budget API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class BudgetController : ControllerBase
  {
    private IBudgetRepository BudgetRepo { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="budgetRepository">Repo to use for budget information.</param>
    public BudgetController(IBudgetRepository budgetRepository)
    {
      BudgetRepo = budgetRepository;
    }
    /// <summary>
    /// Returns a fully populated budget of the given name of the given month and year if it exists and the user owns or has access to that budget.
    /// </summary>
    /// <param name="description">The description of the budget, for a user's primary budgets the description is always "default".</param>
    /// <param name="month">The month the budget is relevant for.</param>
    /// <param name="year">The year the budget is relevant for.</param>
    /// <returns>A fully populated budget object.</returns>
    [HttpGet("{description}/{year}/{month}")]
    public IActionResult Get(string description, int year, int month)
    {
      // Me
      // var userId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var userId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");
      var budget = BudgetRepo.FindByIndex(userId, description, month, year);

      // Not found, create a new "blank" budget they can use instead.
      if (budget == null)
      {
        return Ok(
          new Budget()
          {
            CreatedBy = "James", CreatedOn = DateTime.Now, Description = description, Id = Guid.NewGuid(),
            OwnerId = userId, IsNew = true, StartDate = new DateTime(year, month, 1), Year = year, Month = month
          });
      }
      return Ok(budget);
    }

    public IActionResult Put([FromBody] Budget budget)
    {
      // Me
      // var userId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var userId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");

      BudgetRepo.Upsert(budget, "James");
    }
  }
}