using freebyTech.Common.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;

namespace tivBudget.Dal.Repositories
{
  public class BudgetCategoryTemplateRepository : GenericRepository<BudgetCategoryTemplate>, IBudgetCategoryTemplateRepository
  {
    public BudgetCategoryTemplateRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }
    
    public List<BudgetCategoryTemplate> FindAllTemplatesByOwner(Guid ownerId)
    {
      return QueryIncludingAllBudgetCategortyEntities().Where(bc => bc.OwnerId == null || bc.OwnerId.Value == ownerId).ToList();
    }

    private IQueryable<BudgetCategoryTemplate> QueryIncludingAllBudgetCategortyEntities()
    {
      return Queryable()
        .Include(bc => bc.BudgetItemTemplates);
    }
  }
}
