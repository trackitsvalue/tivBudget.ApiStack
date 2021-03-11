using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetCategory : IEditableModel
    {
        public BudgetCategory()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public bool AreBudgetItemsOpen { get; set; }
        public Guid BudgetId { get; set; }
        public int DisplayIndex { get; set; }
        public decimal CategoryBudgeted { get; set; }
        public decimal CategorySpent { get; set; }
        public decimal CategoryRemaining { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }
        public decimal CategorySpentByRevolvingCredit { get; set; }

            public Budget Budget { get; set; }
            public BudgetCategoryTemplate CategoryTemplate { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
