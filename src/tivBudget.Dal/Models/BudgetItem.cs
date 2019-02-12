using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItem
    {
        public BudgetItem()
        {
            BudgetActuals = new HashSet<BudgetActual>();
        }

        public Guid Id { get; set; }
        public Guid ItemTemplateId { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public decimal AmountBudgeted { get; set; }
        public bool AreBudgetActualsOpen { get; set; }
        public bool IsLinked { get; set; }
        public int DisplayIndex { get; set; }
        public decimal ItemSpent { get; set; }
        public decimal ItemRemaining { get; set; }
        public Guid? AccountLinkId { get; set; }
        public Guid? AccountCategoryLinkId { get; set; }
        public Guid? RecurringSettingsId { get; set; }
        public Guid? AlertId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountCategory AccountCategoryLink { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Account AccountLink { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetItemAlert Alert { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetCategory Category { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetItemTemplate ItemTemplate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetItemRecurringSetting RecurringSettings { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetActual> BudgetActuals { get; set; }
    }
}
