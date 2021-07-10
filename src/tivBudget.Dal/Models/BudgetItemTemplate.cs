using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class BudgetItemTemplate : IEditableModel
  {
    public BudgetItemTemplate()
    {
      BudgetItems = new HashSet<BudgetItem>();
    }

    public Guid Id { get; set; }
    public Guid CategoryTemplateId { get; set; }
    public string Description { get; set; }
    public Guid? OwnerId { get; set; }
    public bool IsVirtualType { get; set; }
    public bool? IsEnvelopeAllowed { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }
    public bool IsAccountTransferType { get; set; }
    public bool? IsCreditAllowed { get; set; }
    public string AllowedAccountLinkTypesOverride { get; set; }
    public bool IsLinkable { get; set; }

    public BudgetCategoryTemplate CategoryTemplate { get; set; }
    public User Owner { get; set; }
    public ICollection<BudgetItem> BudgetItems { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
