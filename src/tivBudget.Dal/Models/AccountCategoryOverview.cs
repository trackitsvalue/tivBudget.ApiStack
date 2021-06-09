using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class AccountCategoryOverview : IEditableModel
  {
    public AccountCategoryOverview()
    {
      AccountActuals = new HashSet<AccountActualOverview>();
    }

    public Guid Id { get; set; }
    public Guid CategoryTemplateId { get; set; }
    public string Description { get; set; }
    public bool AreAccountActualsOpen { get; set; }
    public Guid AccountId { get; set; }
    public int DisplayIndex { get; set; }
    public bool IsDefault { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }
    public AccountCategoryTemplate CategoryTemplate { get; set; }
    public ICollection<AccountActualOverview> AccountActuals { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsChildDirty { get; set; }
    public bool IsDeleted { get; set; }
    public Decimal StartingBalance { get; set; }
    public Decimal EndingBalance { get; set; }
    public Decimal Delta { get; set; }
    public bool IsFound { get; set; } // <-- Used in updates

    #endregion
  }
}
