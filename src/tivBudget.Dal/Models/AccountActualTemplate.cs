using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountActualTemplate : IEditableModel
    {
        public AccountActualTemplate()
        {
            AccountActualRecurrences = new HashSet<AccountActualRecurrence>();
            AccountActuals = new HashSet<AccountActual>();
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

        public AccountTemplate AccountTemplate { get; set; }
        public User Owner { get; set; }
        public ICollection<AccountActualRecurrence> AccountActualRecurrences { get; set; }
        public ICollection<AccountActual> AccountActuals { get; set; }

#region Non-Model Helper Properties

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

#endregion
    }
}
