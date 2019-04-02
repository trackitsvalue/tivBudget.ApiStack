using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountActualRecurrence : IEditableModel
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

        public Account Account { get; set; }
        public AccountActualTemplate ActualTemplate { get; set; }

#region Non-Model Helper Properties

        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }

#endregion
    }
}
