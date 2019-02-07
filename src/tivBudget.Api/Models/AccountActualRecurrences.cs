using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountActualRecurrences
    {
        public Guid Id { get; set; }
        public Guid ActualTemplateId { get; set; }
        public Guid AccountId { get; set; }
        public string Description { get; set; }
        public int? RelevantDay { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Percent { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public Accounts Account { get; set; }
        public AccountActualTemplates ActualTemplate { get; set; }
    }
}
