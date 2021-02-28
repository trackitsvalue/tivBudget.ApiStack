using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IAccountBalanceRepository
  {
    IEnumerable<AccountBalanceInfo> GetAllAccountBalances(Guid ownerId, DateTime endingDate);
    IEnumerable<AccountBalanceInfo> GetAccountsOfTypeBalances(Guid ownerId, DateTime endingDate, int accountType);
  }
}
