using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IAccountRepository
  {
    List<Account> FindAllByOwner(Guid ownerId);

    List<AccountOverview> FindAllByOwnerAndMonth(Guid ownerId, int year, int month);
    int FindCountByOwner(Guid ownerIdOrContributorId);
  }
}
