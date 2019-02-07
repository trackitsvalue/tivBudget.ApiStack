using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetItemAlerts
    {
        public BudgetItemAlerts()
        {
            BudgetItems = new HashSet<BudgetItems>();
        }

        public Guid Id { get; set; }
        public int AlertType { get; set; }
        public bool Acknowledged { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<BudgetItems> BudgetItems { get; set; }
    }
}
