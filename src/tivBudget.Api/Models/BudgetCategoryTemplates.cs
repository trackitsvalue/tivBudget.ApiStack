using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetCategoryTemplates
    {
        public BudgetCategoryTemplates()
        {
            BudgetCategories = new HashSet<BudgetCategories>();
            BudgetItemTemplates = new HashSet<BudgetItemTemplates>();
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

        public Users Owner { get; set; }
        public ICollection<BudgetCategories> BudgetCategories { get; set; }
        public ICollection<BudgetItemTemplates> BudgetItemTemplates { get; set; }
    }
}
