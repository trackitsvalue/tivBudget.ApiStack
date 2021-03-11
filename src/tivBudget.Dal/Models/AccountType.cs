using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountType : IEditableModel
    {
        public AccountType()
        {
            AccountTemplates = new HashSet<AccountTemplate>();
            Accounts = new HashSet<Account>();
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

        public ICollection<AccountTemplate> AccountTemplates { get; set; }
        public ICollection<Account> Accounts { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
