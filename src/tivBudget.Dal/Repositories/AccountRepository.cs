using freebyTech.Common.Data;
using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tivBudget.Dal.Repositories
{
  public class AccountRepository : GenericRepository<Account>, IAccountRepository
  {
    public AccountRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public List<Account> FindAllByOwner(Guid ownerId)
    {
      return QueryIncludingAllAccountEntities().Where(a => a.OwnerId == ownerId).ToList();
    }

    private IQueryable<Account> QueryIncludingAllAccountEntities()
    {
      return Queryable()
        .Include(a => a.AccountCategories);
    }
  }
}
