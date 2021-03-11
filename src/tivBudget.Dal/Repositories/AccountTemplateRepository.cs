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
  public class AccountTemplateRepository : GenericRepository<AccountTemplate>, IAccountTemplateRepository
  {
    public AccountTemplateRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }
    
    public List<AccountTemplate> FindAllTemplatesByOwner(Guid ownerId)
    {
      return QueryIncludingAllAccountEntities().Where(bc => bc.OwnerId == null || bc.OwnerId.Value == ownerId).ToList();
    }

    private IQueryable<AccountTemplate> QueryIncludingAllAccountEntities()
    {
      return Queryable()
        .Include(at => at.AccountCategoryTemplates)
        .Include(at => at.AccountActualTemplates);
    }
  }
}
