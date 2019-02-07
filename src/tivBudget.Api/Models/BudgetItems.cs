using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetItems
    {
        public BudgetItems()
        {
            BudgetActuals = new HashSet<BudgetActuals>();
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

        public AccountCategories AccountCategoryLink { get; set; }
        public Accounts AccountLink { get; set; }
        public BudgetItemAlerts Alert { get; set; }
        public BudgetCategories Category { get; set; }
        public BudgetItemTemplates ItemTemplate { get; set; }
        public BudgetItemRecurringSettings RecurringSettings { get; set; }
        public ICollection<BudgetActuals> BudgetActuals { get; set; }
    }
}
