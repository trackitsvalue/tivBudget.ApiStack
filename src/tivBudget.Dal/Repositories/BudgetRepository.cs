using freebyTech.Common.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
      return OrderElements(CleanDoubleReferences(budget));
    }

    public Budget FindById(Guid ownerIdOrContributorId, Guid budgetId)
    {
      var budget = QueryIncludingAllBudgetEntities()
        .FirstOrDefault(b => b.OwnerId == ownerIdOrContributorId && b.Id == budgetId);
      return OrderElements(CleanDoubleReferences(budget));
    }

    public List<Budget> FindCountByOwner(Guid ownerId, int count)
    {
      return Queryable().Where(b => b.OwnerId == ownerId).OrderByDescending(b => b.StartDate).Take(count).ToList();
    }

    public List<Budget> FindAllByOwner(Guid ownerId)
    {
      return Queryable().Where(b => b.OwnerId == ownerId).OrderByDescending(b => b.StartDate).ToList();
    }

    /// <summary>
    /// Gets the count of budgets that this user is owner or contributor to.
    /// </summary>
    /// <param name="ownerIdOrContributorId"></param>
    /// <returns>Budget count.</returns>
    public int FindCountByOwner(Guid ownerIdOrContributorId)
    {
      return Queryable().Where(b => b.OwnerId == ownerIdOrContributorId).Count();
    }

    /// <summary>
    /// Deletes a budget of the given ID if it exists within the database for the given user.
    /// </summary>
    /// <param name="budgetId"></param>
    /// <returns></returns>
    public void Delete(Guid ownerIdOrContributorId, Guid budgetId)
    {
      var budgetIdParam = new SqlParameter("@BudgetId", SqlDbType.UniqueIdentifier)
      {
        Value = budgetId
      };
      var ownerId = new SqlParameter("@OwnerId", SqlDbType.UniqueIdentifier)
      {
        Value = ownerIdOrContributorId
      };
      // TODO: once security is in place we need to check if a contributor user has delete capability and perform a different delete.
      _dbContext.Database.ExecuteSqlCommand("DELETE FROM freebyTrack.Budgets WHERE ID=@BudgetId AND OwnerID=@OwnerId", budgetIdParam, ownerId);
    }

    /// <summary>
    /// Inserts or updates a new budget for the given user depending upon the state of the data as it comes in from the user.
    /// </summary>
    /// <param name="budget"></param>
    /// <param name="userName"></param>
    public void Upsert(Budget budget, string userName)
    {
      CleanDoubleReferences(budget);
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
          if (budgetCategory.CategoryTemplate != null)
          {
            budgetCategory.CategoryTemplate.BudgetCategories = null;
            budgetCategory.CategoryTemplate.BudgetItemTemplates = null;
          }

          foreach (var budgetItem in budgetCategory.BudgetItems)
          {
            if (budgetItem.ItemTemplate != null)
            {
              budgetItem.ItemTemplate.BudgetItems = null;
              budgetItem.ItemTemplate.CategoryTemplate = null;
            }
          }
        }
      }

      return budget;
    }

    /// <summary>
    /// Order Elements in proper order for budgets before returning.
    /// </summary>
    /// <param name="budget"></param>
    /// <returns></returns>
    private Budget OrderElements(Budget budget)
    {
      if (budget != null)
      {
        foreach (var budgetCategory in budget.BudgetCategories)
        {
          foreach (var budgetItem in budgetCategory.BudgetItems)
          {
            foreach (var budgetActual in budgetItem.BudgetActuals)
            {
              if (budgetActual.AccountActuals != null && budgetActual.AccountActuals.Count > 0)
              {
                budgetActual.AccountActuals = budgetActual.AccountActuals.OrderBy(aa => aa.RelevantOn).ThenBy(aa => aa.CreatedOn).ToList();
              }
            }
            if (budgetItem.BudgetActuals != null && budgetItem.BudgetActuals.Count > 0)
            {
              budgetItem.BudgetActuals = budgetItem.BudgetActuals.OrderBy(ba => ba.RelevantOn).ThenBy(ba => ba.CreatedOn).ToList();
            }
          }
          if (budgetCategory.BudgetItems != null && budgetCategory.BudgetItems.Count > 0)
          {
            budgetCategory.BudgetItems = budgetCategory.BudgetItems.OrderBy(bi => bi.DisplayIndex).ToList();
          }
        }
        if (budget.BudgetCategories != null && budget.BudgetCategories.Count > 0)
        {
          budget.BudgetCategories = budget.BudgetCategories.OrderBy(bc => bc.DisplayIndex).ToList();
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
            .ThenInclude(bi => bi.ItemTemplate);
    }
  }
}
