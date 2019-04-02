using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItemRecurringSetting : IEditableModel
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

        public ICollection<BudgetItem> BudgetItems { get; set; }

#region Non-Model Helper Properties

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

#endregion
    }
}
