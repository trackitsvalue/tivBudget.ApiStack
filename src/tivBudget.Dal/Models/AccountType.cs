using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountType : IEditableData
    {
        public AccountType()
        {
            AccountTemplates = new HashSet<AccountTemplate>();
            Accounts = new HashSet<Account>();
            BudgetItemTemplatesLinkableAccountType = new HashSet<BudgetItemTemplate>();
            BudgetItemTemplatesTransferableAccountType = new HashSet<BudgetItemTemplate>();
        }

        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string DescriptionPlural { get; set; }
        public string DescriptionSingular { get; set; }
        public string PosLineItemShortDescription { get; set; }
        public string PosLineItemDescription { get; set; }
        public string NegLineItemShortDescription { get; set; }
        public string NegLineItemDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountTemplate> AccountTemplates { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Account> Accounts { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItemTemplate> BudgetItemTemplatesLinkableAccountType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItemTemplate> BudgetItemTemplatesTransferableAccountType { get; set; }
    }
}
