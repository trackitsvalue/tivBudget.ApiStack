using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class Account : IEditableModel
    {
        public Account()
        {
            AccountActualRecurrences = new HashSet<AccountActualRecurrence>();
            AccountCategories = new HashSet<AccountCategory>();
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid AccountTemplateId { get; set; }
        public int AccountTypeId { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public bool AreAccountCategoriesOpen { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public AccountTemplate AccountTemplate { get; set; }
        public AccountType AccountType { get; set; }
        public User Owner { get; set; }
        public ICollection<AccountActualRecurrence> AccountActualRecurrences { get; set; }
        public ICollection<AccountCategory> AccountCategories { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }

#region Non-Model Helper Properties

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

#endregion
    }
}
