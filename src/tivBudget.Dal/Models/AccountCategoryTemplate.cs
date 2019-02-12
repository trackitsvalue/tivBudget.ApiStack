using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class AccountCategoryTemplate
    {
        public AccountCategoryTemplate()
        {
            AccountCategories = new HashSet<AccountCategory>();
        }

        public Guid Id { get; set; }
        public Guid AccountTemplateId { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public Guid? OwnerId { get; set; }
        public string Icon { get; set; }
        public string BackgroundColor { get; set; }
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
        public ICollection<AccountCategory> AccountCategories { get; set; }
    }
}
