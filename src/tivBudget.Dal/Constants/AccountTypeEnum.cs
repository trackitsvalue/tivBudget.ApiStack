using System;
using System.Collections.Generic;
using System.Text;

namespace tivBudget.Dal.Constants
{
  /// <summary>
  /// This enum always needs to be in sync with the DB and with the client JavaScript.
  /// </summary>
  public enum AccountTypeEnum : int
  {
    BankAccount = 1,
    RetirementAccount = 2,
    UnsecuredCreditAcccount = 3,
    SecuredCreditAccount = 4,
    CollegeSavingsAccounts = 5,
    CryptoCurrencyAccounts = 6,
    CashAccount = 7,
    TrustAccounts = 8,
  }
}
