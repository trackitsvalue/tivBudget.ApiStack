using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
    public partial class Application : IEditableModel
    {
        public Application()
        {
            UserSettings = new HashSet<UserSetting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<UserSetting> UserSettings { get; set; }

    #region Non-Model Helper Properties

            public bool IsNew { get; set; }
            public bool IsDirty { get; set; }
            public bool IsDeleted { get; set; }

    #endregion
    }
}
