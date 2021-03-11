using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class UserSetting : IEditableModel
    {
        public Guid UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? IsCacheable { get; set; }
        public bool? IsWritable { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

            public Application Application { get; set; }
            public User User { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
