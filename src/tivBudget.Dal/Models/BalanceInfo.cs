using System;

namespace tivBudget.Dal.Models
{
  public partial class BalanceInfo
  {
    public int Month { get; set; }
    public int Day { get; set; }
    public int Year { get; set; }
    public Decimal Balance { get; set; }
  }
}
