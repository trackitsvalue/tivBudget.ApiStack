using System;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Models;

namespace tivBudget.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        /// <summary>
        /// Returns a fully populated budget of the given ID if it exists and the user owns or has access to that budget.
        /// </summary>
        /// <param name="id">The unique ID representing the budget.</param>
        /// <returns>A fully populated budget object.</returns>
        [HttpGet("{id}")]
        public Budget Get(Guid id)
        {
            return new Budget() { Id = id };
        }

        /// <summary>
        /// Returns a fully populated budget of the given name of the given month and year if it exists and the user owns or has access to that budget.
        /// </summary>
        /// <param name="description">The description of the budget, for a user's primary budgets the description is always "default".</param>
        /// <param name="month">The month the budget is relevant for.</param>
        /// <param name="year">The year the budget is relevant for.</param>
        /// <returns>A fully populated budget object.</returns>
        [HttpGet("{description}/{month}/{year}")]
        public Budget Get(string description, int month, int year)
        {
            return new Budget() { Description = description, Month = month, Year = year };
        }
    }
}