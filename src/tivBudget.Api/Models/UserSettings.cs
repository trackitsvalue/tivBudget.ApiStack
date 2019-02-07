using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class UserSettings
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

        public Applications Application { get; set; }
        public Users User { get; set; }
    }
}
