using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class AccountCategories
    {
        public AccountCategories()
        {
            AccountActuals = new HashSet<AccountActuals>();
            BudgetItems = new HashSet<BudgetItems>();
        }

        public Guid Id { get; set; }
        public Guid CategoryTemplateId { get; set; }
        public string Description { get; set; }
        public bool AreAccountActualsOpen { get; set; }
        public Guid AccountId { get; set; }
        public int DisplayIndex { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public Accounts Account { get; set; }
        public AccountCategoryTemplates CategoryTemplate { get; set; }
        public ICollection<AccountActuals> AccountActuals { get; set; }
        public ICollection<BudgetItems> BudgetItems { get; set; }
    }
}
