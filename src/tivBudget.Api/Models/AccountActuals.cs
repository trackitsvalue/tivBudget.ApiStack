using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountActuals
    {
        public Guid Id { get; set; }
        public Guid ActualTemplateId { get; set; }
        public Guid? BudgetActualLinkId { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime RelevantOn { get; set; }
        public decimal Amount { get; set; }
        public bool IsLinked { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public AccountActualTemplates ActualTemplate { get; set; }
        public BudgetActuals BudgetActualLink { get; set; }
        public AccountCategories Category { get; set; }
    }
}
