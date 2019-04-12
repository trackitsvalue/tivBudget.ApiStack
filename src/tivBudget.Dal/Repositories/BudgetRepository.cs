using freebyTech.Common.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using freebyTech.Common.Data.Interfaces;
using freebyTech.Common.ExtensionMethods;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;

namespace tivBudget.Dal.Repositories
{
  public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
  {
    public BudgetRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public Budget FindByIndex(Guid ownerIdOrContributorId, string description, int month, int year)
    {
      var budget = QueryIncludingAllBudgetEntities()
        .FirstOrDefault(b =>
          b.OwnerId == ownerIdOrContributorId && b.Year == year && b.Month == month && b.Description == description);
      return CleanDoubleReferences(budget);
    }

    public Budget FindById(Guid ownerIdOrContributorId, Guid budgetId)
    {
      return QueryIncludingAllBudgetEntities()
        .FirstOrDefault(b => b.OwnerId == ownerIdOrContributorId && b.Id == budgetId);
    }

    public List<Budget> FindAllByOwner(Guid ownerId)
    {
      return Queryable().Where(b => b.OwnerId == ownerId).ToList();
    }

    /// <summary>
    /// Deletes a budget of the given ID if it exists within the database for the given user.
    /// </summary>
    /// <param name="budgetId"></param>
    /// <returns></returns>
    public void Delete(Guid ownerIdOrContributorId, Guid budgetId)
    {
      // TODO: once security is in place we need to check if a contributor user has delete capability and perform a different delete.
      _dbContext.Database.ExecuteSqlCommand("DELETE FROM freebyTrack.Budgets WHERE ID='{0}' AND OwnerID='{1}'", budgetId, ownerIdOrContributorId);
    }

    /// <summary>
    /// Inserts or updates a new budget for the given user depending upon the state of the data as it comes in from the user.
    /// </summary>
    /// <param name="budget"></param>
    /// <param name="userName"></param>
    public void Upsert(Budget budget, string userName)
    {
      UpsertFromEditableModelStates(budget, userName);
    }

    /// <summary>
    /// Because of the nature of the data we pull we get multiple links to the same items in some templates.
    /// </summary>
    /// <param name="budget"></param>
    /// <returns></returns>
    private Budget CleanDoubleReferences(Budget budget)
    {
      if (budget != null)
      {
        foreach (var budgetCategory in budget.BudgetCategories)
        {
          foreach (var budgetItem in budgetCategory.BudgetItems)
          {
            budgetItem.ItemTemplate.BudgetItems = null;
          }
        }
      }

      return budget;
    }

    private IQueryable<Budget> QueryIncludingAllBudgetEntities()
    {
      return Queryable()
        .Include(b => b.BudgetCategories)
          .ThenInclude(bc => bc.BudgetItems)
            .ThenInclude(bi => bi.BudgetActuals)
              .ThenInclude(ba => ba.AccountActuals)
        .Include(b => b.BudgetCategories)
          .ThenInclude(bc => bc.CategoryTemplate)
        .Include(b => b.BudgetCategories)
          .ThenInclude(bc => bc.BudgetItems)
            .ThenInclude(bi => bi.ItemTemplate)
        .Include(b => b.BudgetCategories)
          .ThenInclude(bc => bc.BudgetItems)
            .ThenInclude(bi => bi.AccountLink)
        .Include(b => b.BudgetCategories)
          .ThenInclude(bc => bc.BudgetItems)
            .ThenInclude(bi => bi.AccountCategoryLink);
    }
  }
}
