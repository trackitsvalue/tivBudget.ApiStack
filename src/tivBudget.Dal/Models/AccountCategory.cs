using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountCategory : IEditableModel
    {
        public AccountCategory()
        {
            AccountActuals = new HashSet<AccountActual>();
            BudgetItems = new HashSet<BudgetItem>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public bool AreAccountActualsOpen { get; set; }
        public Guid AccountId { get; set; }
        public int DisplayIndex { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Account Account { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountCategoryTemplate CategoryTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountActual> AccountActuals { get; set; }

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
