using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetActuals
    {
        public BudgetActuals()
        {
            AccountActuals = new HashSet<AccountActuals>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime RelevantOn { get; set; }
        public Guid ItemId { get; set; }
        public bool IsLinked { get; set; }
        public bool IsEnvelopeDeposit { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public BudgetItems Item { get; set; }
        public ICollection<AccountActuals> AccountActuals { get; set; }
    }
}
