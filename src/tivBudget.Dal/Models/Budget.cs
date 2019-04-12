using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class Budget : IEditableModel
    {
        public Budget()
        {
            BudgetCategories = new HashSet<BudgetCategory>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Guid OwnerId { get; set; }
        public int DisplayIndex { get; set; }
        public decimal ActualIncome { get; set; }
        public decimal EstimatedIncome { get; set; }
        public decimal ActualMinusEstimatedIncome { get; set; }
        public decimal ActualSpending { get; set; }
        public decimal EstimatedSpending { get; set; }
        public decimal ActualRemaining { get; set; }
        public decimal EstimatedRemaining { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime StartDate { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<BudgetCategory> BudgetCategories { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
