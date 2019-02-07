using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetCategories
    {
        public BudgetCategories()
        {
            BudgetItems = new HashSet<BudgetItems>();
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

        public Budgets Budget { get; set; }
        public BudgetCategoryTemplates CategoryTemplate { get; set; }
        public ICollection<BudgetItems> BudgetItems { get; set; }
    }
}
