using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class Budgets
    {
        public Budgets()
        {
            BudgetCategories = new HashSet<BudgetCategories>();
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

        public ICollection<BudgetCategories> BudgetCategories { get; set; }
    }
}
