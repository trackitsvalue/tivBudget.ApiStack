using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class Account : IEditableModel
    {
        public Account()
        {
            AccountActualRecurrences = new HashSet<AccountActualRecurrence>();
            AccountCategories = new HashSet<AccountCategory>();
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid AccountTemplateId { get; set; }
        public int AccountTypeId { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public bool AreAccountCategoriesOpen { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountTemplate AccountTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountType AccountType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User Owner { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountActualRecurrence> AccountActualRecurrences { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountCategory> AccountCategories { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BudgetItem> BudgetItems { get; set; }

#region Non-Model Helper Properties

        [NotMapped]
        public bool IsNew { get; set; }
        [NotMapped]
        public bool IsDirty { get; set; }

#endregion
    }
}
