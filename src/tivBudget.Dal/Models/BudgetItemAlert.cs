using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItemAlert : IEditableModel
    {
        public BudgetItemAlert()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public int AlertType { get; set; }
        public bool Acknowledged { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<BudgetItem> BudgetItems { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
