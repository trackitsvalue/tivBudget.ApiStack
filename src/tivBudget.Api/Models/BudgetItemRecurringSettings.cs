using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetItemRecurringSettings
    {
        public BudgetItemRecurringSettings()
        {
            BudgetItems = new HashSet<BudgetItems>();
        }

        public Guid Id { get; set; }
        public int? DayDue { get; set; }
        public int? FriendlyReminder { get; set; }
        public int? WarningReminder { get; set; }
        public bool SendViaEmail { get; set; }
        public bool SendViaText { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<BudgetItems> BudgetItems { get; set; }
    }
}
