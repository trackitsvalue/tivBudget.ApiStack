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
  public class AccountCategoryTemplateRepository : GenericRepository<AccountCategoryTemplate>, IAccountCategoryTemplateRepository
  {
    public AccountCategoryTemplateRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }
    
    public List<AccountCategoryTemplate> FindAllTemplatesByOwner(Guid ownerId)
    {
      return Queryable().Where(bc => bc.OwnerId == null || bc.OwnerId.Value == ownerId).ToList();
    }
  }
}
