using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace tivBudget.Dal.Models
{
    public partial class Group
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
