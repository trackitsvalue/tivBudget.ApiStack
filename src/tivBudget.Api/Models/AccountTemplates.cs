using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountTemplates
    {
        public AccountTemplates()
        {
            AccountActualTemplates = new HashSet<AccountActualTemplates>();
            AccountCategoryTemplates = new HashSet<AccountCategoryTemplates>();
            Accounts = new HashSet<Accounts>();
            BudgetItemTemplatesLinkableAccountTemplate = new HashSet<BudgetItemTemplates>();
            BudgetItemTemplatesTransferableAccountTemplate = new HashSet<BudgetItemTemplates>();
        }

        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public Guid? OwnerId { get; set; }
        public string Icon { get; set; }
        public bool IsIncomeAccount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public Users Owner { get; set; }
        public AccountTypes Type { get; set; }
        public ICollection<AccountActualTemplates> AccountActualTemplates { get; set; }
        public ICollection<AccountCategoryTemplates> AccountCategoryTemplates { get; set; }
        public ICollection<Accounts> Accounts { get; set; }
        public ICollection<BudgetItemTemplates> BudgetItemTemplatesLinkableAccountTemplate { get; set; }
        public ICollection<BudgetItemTemplates> BudgetItemTemplatesTransferableAccountTemplate { get; set; }
    }
}
