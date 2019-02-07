using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class BudgetItemTemplates
    {
        public BudgetItemTemplates()
        {
            BudgetItems = new HashSet<BudgetItems>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public bool IsLinkable { get; set; }
        public int? LinkableAccountTypeId { get; set; }
        public Guid? LinkableAccountTemplateId { get; set; }
        public bool IsTransferable { get; set; }
        public int? TransferableAccountTypeId { get; set; }
        public Guid? TransferableAccountTemplateId { get; set; }
        public bool IsEnvelopeAllowed { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public BudgetCategoryTemplates CategoryTemplate { get; set; }
        public AccountTemplates LinkableAccountTemplate { get; set; }
        public AccountTypes LinkableAccountType { get; set; }
        public Users Owner { get; set; }
        public AccountTemplates TransferableAccountTemplate { get; set; }
        public AccountTypes TransferableAccountType { get; set; }
        public ICollection<BudgetItems> BudgetItems { get; set; }
    }
}
