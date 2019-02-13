using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class AccountActualTemplate
    {
        public AccountActualTemplate()
        {
            AccountActualRecurrences = new HashSet<AccountActualRecurrence>();
            AccountActuals = new HashSet<AccountActual>();
        }

        public Guid Id { get; set; }
        public Guid AccountTemplateId { get; set; }
        public string Description { get; set; }
        public bool IsDeposit { get; set; }
        public bool IsDefault { get; set; }
        public bool AllowRecurringAmount { get; set; }
        public bool AllowRecurringPercent { get; set; }
        public bool? AllowRecurringDay { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AccountTemplate AccountTemplate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User Owner { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountActualRecurrence> AccountActualRecurrences { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<AccountActual> AccountActuals { get; set; }
    }
}
