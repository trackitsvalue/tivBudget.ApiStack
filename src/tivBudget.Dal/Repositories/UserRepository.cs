using freebyTech.Common.Data;
using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using freebyTech.Common.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

namespace tivBudget.Dal.Repositories
{
  public class UserRepository : GenericRepository<User>, IUserRepository
  {
    public UserRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public User FindByEmail(string emailAddress)
    {
      return QueryIncludingAllUserEntities().Where(u => u.Email == emailAddress).FirstOrDefault();
    }

    public User FindByUserName(string userName)
    {
      return QueryIncludingAllUserEntities().Where(u => u.UserName == userName).FirstOrDefault();
    }

    private IQueryable<User> QueryIncludingAllUserEntities()
    {
      return Queryable()
        .Include(u => u.UserAccomplishments)
        .Include(u => u.UserSettings);
    }
  }
}
