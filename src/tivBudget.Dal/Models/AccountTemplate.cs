using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class AccountTemplate : IEditableModel
  {
    public AccountTemplate()
    {
      AccountActualTemplates = new HashSet<AccountActualTemplate>();
      AccountCategoryTemplates = new HashSet<AccountCategoryTemplate>();
      Accounts = new HashSet<Account>();
    }

    public Guid Id { get; set; }
    public int TypeId { get; set; }
    public string Description { get; set; }
    public Guid? OwnerId { get; set; }
    public string Icon { get; set; }
    public bool IsIncomeAccount { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    public User Owner { get; set; }
    public AccountType Type { get; set; }
    public ICollection<AccountActualTemplate> AccountActualTemplates { get; set; }
    public ICollection<AccountCategoryTemplate> AccountCategoryTemplates { get; set; }
    public ICollection<Account> Accounts { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
