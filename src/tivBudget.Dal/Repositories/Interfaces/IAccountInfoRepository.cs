using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IAccountInfoRepository
  {
    IEnumerable<AccountCategoryBalanceInfo> GetAllAccountCategoryBalances(Guid ownerId, DateTime endingDate);
    IEnumerable<AccountAndCategoryMetadata> GetAllAccountCategoryMetadata(Guid ownerId);
    IEnumerable<AccountCategoryBalanceInfo> GetAccountsOfTypeAccountCategoryBalances(Guid ownerId, DateTime endingDate, int accountType);
  }
}
