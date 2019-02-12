using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItemAlert
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

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
