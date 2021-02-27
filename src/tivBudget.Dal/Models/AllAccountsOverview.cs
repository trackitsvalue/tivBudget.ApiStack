using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public class AllAccountsOverview
  {
    public int RelevantMonth { get; set; }
    public int RelevantYear { get; set; }
    public BalanceInfo StartOfMonth { get; set; }
    public BalanceInfo EndOfMonth { get; set; }
    public Decimal Delta { get; set; }
    public AccountsOfTypeOverview[] AccountTypes { get; set; }
  }
}
