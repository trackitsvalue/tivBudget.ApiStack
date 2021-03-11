using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public class AccountsOfTypeOverview
  {
    public AccountType Info { get; set; }
    public BalanceInfo StartOfMonth { get; set; }
    public BalanceInfo EndOfMonth { get; set; }
    public Decimal Delta { get; set; }
    public bool AreAccountsOpen { get; set; }
    public AccountOverview[] Accounts { get; set; }
  }
}
