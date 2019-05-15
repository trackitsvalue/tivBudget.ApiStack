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

    /// <summary>
    /// Gets the count of accounts that this user is owner of or contributor to.
    /// </summary>
    /// <param name="ownerIdOrContributorId"></param>
    /// <returns>Account count.</returns>
    public int FindCountByOwner(Guid ownerIdOrContributorId)
    {
      return Queryable().Where(b => b.OwnerId == ownerIdOrContributorId).Count();
    }

    private IQueryable<Account> QueryIncludingAllAccountEntities()
    {
      return Queryable()
        .Include(a => a.AccountCategories)
        .Include(a => a.AccountTemplate);
    }
  }
}
