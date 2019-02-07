using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountActualTemplates
    {
        public AccountActualTemplates()
        {
            AccountActualRecurrences = new HashSet<AccountActualRecurrences>();
            AccountActuals = new HashSet<AccountActuals>();
        }

        public Guid Id { get; set; }
        public Guid AccountTemplateId { get; set; }
        public string Description { get; set; }
        public bool IsDeposit { get; set; }
        public bool IsDefault { get; set; }
        public bool AllowRecurringAmount { get; set; }
        public bool AllowRecurringPercent { get; set; }
        public bool? AllowRecurringDay { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public AccountTemplates AccountTemplate { get; set; }
        public Users Owner { get; set; }
        public ICollection<AccountActualRecurrences> AccountActualRecurrences { get; set; }
        public ICollection<AccountActuals> AccountActuals { get; set; }
    }
}
