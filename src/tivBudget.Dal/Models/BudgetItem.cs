using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItem : IEditableModel
    {
        public BudgetItem()
        {
            BudgetActuals = new HashSet<BudgetActual>();
        }

        public Guid Id { get; set; }
        public Guid ItemTemplateId { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public decimal AmountBudgeted { get; set; }
        public bool AreBudgetActualsOpen { get; set; }
        public bool IsLinked { get; set; }
        public int DisplayIndex { get; set; }
        public decimal ItemSpent { get; set; }
        public decimal ItemRemaining { get; set; }
        public Guid? AccountLinkId { get; set; }
        public Guid? AccountCategoryLinkId { get; set; }
        public Guid? RecurringSettingsId { get; set; }
        public Guid? AlertId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public AccountCategory AccountCategoryLink { get; set; }
        public Account AccountLink { get; set; }
        public BudgetItemAlert Alert { get; set; }
        public BudgetCategory Category { get; set; }
        public BudgetItemTemplate ItemTemplate { get; set; }
        public BudgetItemRecurringSetting RecurringSettings { get; set; }
        public ICollection<BudgetActual> BudgetActuals { get; set; }

#region Non-Model Helper Properties

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

#endregion
    }
}
