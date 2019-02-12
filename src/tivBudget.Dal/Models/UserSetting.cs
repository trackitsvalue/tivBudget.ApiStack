using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class UserSetting
    {
        public Guid UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? IsCacheable { get; set; }
        public bool? IsWritable { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Application Application { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; }
    }
}
