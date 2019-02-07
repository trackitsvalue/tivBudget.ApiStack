using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class Applications
    {
        public Applications()
        {
            UserSettings = new HashSet<UserSettings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<UserSettings> UserSettings { get; set; }
    }
}
