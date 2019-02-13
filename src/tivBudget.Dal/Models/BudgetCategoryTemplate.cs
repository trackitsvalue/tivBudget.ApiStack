using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class BudgetCategoryTemplate
    {
        public BudgetCategoryTemplate()
        {
            BudgetCategories = new HashSet<BudgetCategory>();
            BudgetItemTemplates = new HashSet<BudgetItemTemplate>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public string Icon { get; set; }
        public bool IsIncomeCategory { get; set; }
        public string BackgroundColor { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        [IgnoreDataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User Owner { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetCategory> BudgetCategories { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItemTemplate> BudgetItemTemplates { get; set; }
    }
}
