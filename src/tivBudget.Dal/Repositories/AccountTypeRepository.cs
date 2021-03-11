using freebyTech.Common.Data;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;

namespace tivBudget.Dal.Repositories
{
  public class AccountTypeRepository : GenericReadOnlyRepository<AccountType>, IAccountTypeRepository
  {
    public AccountTypeRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<AccountType> GetAllAccountTypes()
    {
      return this.All();
    }
  }
}
