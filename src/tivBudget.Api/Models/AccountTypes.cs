using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountTypes
    {
        public AccountTypes()
        {
            AccountTemplates = new HashSet<AccountTemplates>();
            Accounts = new HashSet<Accounts>();
            BudgetItemTemplatesLinkableAccountType = new HashSet<BudgetItemTemplates>();
            BudgetItemTemplatesTransferableAccountType = new HashSet<BudgetItemTemplates>();
        }

        public int Id { get; set; }
        public int DisplayIndex { get; set; }
        public string DescriptionPlural { get; set; }
        public string DescriptionSingular { get; set; }
        public string PosLineItemShortDescription { get; set; }
        public string PosLineItemDescription { get; set; }
        public string NegLineItemShortDescription { get; set; }
        public string NegLineItemDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<AccountTemplates> AccountTemplates { get; set; }
        public ICollection<Accounts> Accounts { get; set; }
        public ICollection<BudgetItemTemplates> BudgetItemTemplatesLinkableAccountType { get; set; }
        public ICollection<BudgetItemTemplates> BudgetItemTemplatesTransferableAccountType { get; set; }
    }
}
