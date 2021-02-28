using System;

namespace tivBudget.Dal.Models
{
  public class AccountBalanceInfo
  {
    public Guid Id { get; set; }
    public Decimal CurrentBalance { get; set; }
  }
}