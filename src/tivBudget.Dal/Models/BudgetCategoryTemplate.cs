using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetCategoryTemplate : IEditableModel
    {
        public BudgetCategoryTemplate()
        {
            BudgetCategories = new HashSet<BudgetCategory>();
            BudgetItemTemplates = new HashSet<BudgetItemTemplate>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public string Icon { get; set; }
        public bool IsIncomeCategory { get; set; }
        public string BackgroundColor { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public User Owner { get; set; }
        public ICollection<BudgetCategory> BudgetCategories { get; set; }
        public ICollection<BudgetItemTemplate> BudgetItemTemplates { get; set; }

#region Non-Model Helper Properties

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

#endregion
    }
}
