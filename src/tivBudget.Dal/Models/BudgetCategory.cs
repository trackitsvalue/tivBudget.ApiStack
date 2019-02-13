using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class BudgetCategory : IEditableData
    {
        public BudgetCategory()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public bool AreBudgetItemsOpen { get; set; }
        public Guid BudgetId { get; set; }
        public int DisplayIndex { get; set; }
        public decimal CategoryBudgeted { get; set; }
        public decimal CategorySpent { get; set; }
        public decimal CategoryRemaining { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Budget Budget { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetCategoryTemplate CategoryTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
