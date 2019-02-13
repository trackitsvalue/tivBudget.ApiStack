using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class BudgetItemTemplate
    {
        public BudgetItemTemplate()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public bool IsLinkable { get; set; }
        public int? LinkableAccountTypeId { get; set; }
        public Guid? LinkableAccountTemplateId { get; set; }
        public bool IsTransferable { get; set; }
        public int? TransferableAccountTypeId { get; set; }
        public Guid? TransferableAccountTemplateId { get; set; }
        public bool IsEnvelopeAllowed { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BudgetCategoryTemplate CategoryTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountTemplate LinkableAccountTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountType LinkableAccountType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User Owner { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountTemplate TransferableAccountTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountType TransferableAccountType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItem> BudgetItems { get; set; }
    }
}
