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
  public class AccountInfoRepository : GenericReadOnlyRepository<AccountCategoryBalanceInfo>, IAccountInfoRepository
  {
    public AccountInfoRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<AccountCategoryBalanceInfo> GetAllAccountCategoryBalances(Guid ownerId, DateTime endingDate)
    {
      return _dbContext.Query<AccountCategoryBalanceInfo>().FromSql(
        "EXEC [freebyTrack].[Accounting_GetCurrentBalanceAllAccountCategories] @OwnerID, @CurrentValueDate",
        new SqlParameter("OwnerID", ownerId), new SqlParameter("CurrentValueDate", endingDate)).ToList();
    }

    public IEnumerable<AccountAndCategoryMetadata> GetAllAccountCategoryMetadata(Guid ownerId)
    {
      return _dbContext.Query<AccountAndCategoryMetadata>().FromSql(@"
        SELECT 'AC' as recordType, ac.ID, MIN(aa.relevantOn) as oldestRelevantOn, MAX(aa.relevantOn) as newestRelevantOn
          FROM [freebyTrack].[AccountActuals] aa
          INNER JOIN [freebyTrack].[AccountCategories] ac on aa.CategoryID=ac.ID
          INNER JOIN [freebyTrack].[Accounts] a on ac.AccountID=a.ID
          WHERE a.OwnerID = @OwnerID
          group by ac.ID
        UNION
        SELECT 'A' as recordType, a.ID, MIN(aa.relevantOn) as oldestRelevantOn, MAX(aa.relevantOn) as newestRelevantOn
          FROM [freebyTrack].[AccountActuals] aa
          INNER JOIN [freebyTrack].[AccountCategories] ac on aa.CategoryID=ac.ID
          INNER JOIN [freebyTrack].[Accounts] a on ac.AccountID=a.ID
          WHERE a.OwnerID = @OwnerID
          group by a.ID
          ",
        new SqlParameter("OwnerID", ownerId)).AsNoTracking().ToList();
    }

    public IEnumerable<AccountCategoryBalanceInfo> GetAccountsOfTypeAccountCategoryBalances(Guid ownerId, DateTime endingDate, int accountType)
    {
      return _dbContext.Query<AccountCategoryBalanceInfo>().FromSql(
        "EXEC [freebyTrack].[Accounting_GetCurrentBalanceAllAccountCategories] @OwnerID, @CurrentValueDate, @AccountTypeID",
        new SqlParameter("OwnerID", ownerId), new SqlParameter("CurrentValueDate", endingDate), new SqlParameter("AccountTypeID", accountType)).AsNoTracking().ToList();
    }
  }
}
