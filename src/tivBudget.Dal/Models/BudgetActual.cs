using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetActual : IEditableData
    {
        public BudgetActual()
        {
            AccountActuals = new HashSet<AccountActual>();
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


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetItem Item { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountActual> AccountActuals { get; set; }
    }
}
