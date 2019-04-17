using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountCategory : IEditableModel
    {
        public AccountCategory()
        {
            AccountActuals = new HashSet<AccountActual>();
            BudgetActuals = new HashSet<BudgetActual>();
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public bool AreAccountActualsOpen { get; set; }
        public Guid AccountId { get; set; }
        public int DisplayIndex { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

            public Account Account { get; set; }
            public AccountCategoryTemplate CategoryTemplate { get; set; }
        public ICollection<AccountActual> AccountActuals { get; set; }
        public ICollection<BudgetActual> BudgetActuals { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
