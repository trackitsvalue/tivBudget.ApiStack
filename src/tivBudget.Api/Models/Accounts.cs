using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            AccountActualRecurrences = new HashSet<AccountActualRecurrences>();
            AccountCategories = new HashSet<AccountCategories>();
            BudgetItems = new HashSet<BudgetItems>();
        }

        public Guid Id { get; set; }
        public Guid AccountTemplateId { get; set; }
        public int AccountTypeId { get; set; }
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public bool AreAccountCategoriesOpen { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public AccountTemplates AccountTemplate { get; set; }
        public AccountTypes AccountType { get; set; }
        public Users Owner { get; set; }
        public ICollection<AccountActualRecurrences> AccountActualRecurrences { get; set; }
        public ICollection<AccountCategories> AccountCategories { get; set; }
        public ICollection<BudgetItems> BudgetItems { get; set; }
    }
}
