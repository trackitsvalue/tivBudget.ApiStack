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
        [HttpGet("{description}/{month}/{year}")]
        public IActionResult Get(string description, int month, int year)
        {
            var budget = BudgetRepo.FindByIndex(new Guid("3DC480F1-5586-E311-821B-00215E73190E"), description, month, year);

            if (budget == null) return NotFound();
            return Ok(budget);
        }
    }
}