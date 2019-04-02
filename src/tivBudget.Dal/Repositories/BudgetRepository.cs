using freebyTech.Common.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Dal.Repositories
{
  public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
  {
    public BudgetRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public Budget FindByIndex(Guid ownerIdOrContributorId, string description, int month, int year)
    {
      return QueryIncludingAllBudgetEntities()
          .FirstOrDefault(b => b.OwnerId == ownerIdOrContributorId && b.Year == year && b.Month == month && b.Description == description);
    }

    public Budget FindById(Guid ownerIdOrContributorId, Guid budgetId)
    {
      return QueryIncludingAllBudgetEntities()
        .FirstOrDefault(b => b.OwnerId == ownerIdOrContributorId && b.Id == budgetId);
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

    public void UpsertBudget(Budget budget, string userName)
    {
      UpsertFromEditableModelStates(budget, userName);
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
