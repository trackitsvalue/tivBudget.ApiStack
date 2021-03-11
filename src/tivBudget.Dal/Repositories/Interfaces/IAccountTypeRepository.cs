using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IAccountTypeRepository
  {
    IEnumerable<AccountType> GetAllAccountTypes();
  }
}
