using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using freebyTech.Common.Data;
using System.Data.SqlClient;

namespace tivBudget.Dal.Repositories
{
  public class AccountBalanceRepository : GenericReadOnlyRepository<AccountBalanceInfo>, IAccountBalanceRepository
  {
    public AccountBalanceRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<AccountBalanceInfo> GetAllAccountBalances(Guid ownerId, DateTime endingDate)
    {
      return _dbContext.Query<AccountBalanceInfo>().FromSql(
        "EXEC [freebyTrack].[Accounting_GetCurrentBalanceAllAccountCategories] @OwnerID, @CurrentValueDate",
        new SqlParameter("OwnerID", ownerId), new SqlParameter("CurrentValueDate", endingDate)).ToList();
    }

    public IEnumerable<AccountBalanceInfo> GetAccountsOfTypeBalances(Guid ownerId, DateTime endingDate, int accountType)
    {
      return _dbContext.Query<AccountBalanceInfo>().FromSql(
        "EXEC [freebyTrack].[Accounting_GetCurrentBalanceAllAccountCategories] @OwnerID, @CurrentValueDate, @AccountTypeID",
        new SqlParameter("OwnerID", ownerId), new SqlParameter("CurrentValueDate", endingDate), new SqlParameter("AccountTypeID", accountType)).AsNoTracking().ToList();
    }
  }
}
