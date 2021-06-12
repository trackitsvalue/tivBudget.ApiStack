using System;

namespace tivBudget.Dal.Models
{
  public class AccountCategoryBalanceInfo
  {
    public Guid Id { get; set; }
    public Decimal CurrentBalance { get; set; }
  }
}