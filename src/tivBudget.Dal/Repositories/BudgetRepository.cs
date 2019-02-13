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
                        .ThenInclude(bi => bi.AccountCategoryLink)
                .FirstOrDefault(b => b.OwnerId == ownerIdOrContributorId && b.Year == year && b.Month == month && b.Description == description);
        }
    }
}
