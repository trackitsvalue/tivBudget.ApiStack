using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountCategoryTemplates
    {
        public AccountCategoryTemplates()
        {
            AccountCategories = new HashSet<AccountCategories>();
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

        public AccountTemplates AccountTemplate { get; set; }
        public Users Owner { get; set; }
        public ICollection<AccountCategories> AccountCategories { get; set; }
    }
}
