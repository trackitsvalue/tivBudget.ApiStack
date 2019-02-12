using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItemRecurringSetting
    {
        public BudgetItemRecurringSetting()
        {
            BudgetItems = new HashSet<BudgetItem>();
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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
