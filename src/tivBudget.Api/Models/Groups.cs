using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class Groups
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsNewUserDefault { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }
    }
}
