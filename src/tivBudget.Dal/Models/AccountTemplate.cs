using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class AccountTemplate
    {
        public AccountTemplate()
        {
            AccountActualTemplates = new HashSet<AccountActualTemplate>();
            AccountCategoryTemplates = new HashSet<AccountCategoryTemplate>();
            Accounts = new HashSet<Account>();
            BudgetItemTemplatesLinkableAccountTemplate = new HashSet<BudgetItemTemplate>();
            BudgetItemTemplatesTransferableAccountTemplate = new HashSet<BudgetItemTemplate>();
        }

        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public string Icon { get; set; }
        public bool IsIncomeAccount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User Owner { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountType Type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountActualTemplate> AccountActualTemplates { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountCategoryTemplate> AccountCategoryTemplates { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Account> Accounts { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItemTemplate> BudgetItemTemplatesLinkableAccountTemplate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItemTemplate> BudgetItemTemplatesTransferableAccountTemplate { get; set; }
    }
}
