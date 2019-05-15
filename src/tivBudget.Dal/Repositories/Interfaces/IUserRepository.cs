using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IUserRepository
  {
    User FindById(Guid userId);
    User FindByUserName(string userName);
    User FindByEmail(string emailAddress);
    void Insert(User user, string userName);
    void Update(User user, string userName);
  }
}
