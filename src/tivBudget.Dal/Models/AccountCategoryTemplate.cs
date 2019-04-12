using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class AccountCategoryTemplate : IEditableModel
    {
        public AccountCategoryTemplate()
        {
            AccountCategories = new HashSet<AccountCategory>();
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

            public AccountTemplate AccountTemplate { get; set; }
            public User Owner { get; set; }
        public ICollection<AccountCategory> AccountCategories { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
