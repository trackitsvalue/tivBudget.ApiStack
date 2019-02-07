using System;
using System.Collections.Generic;

namespace tivBudget.Api.Models
{
    public partial class Users
    {
        public Users()
        {
            AccountActualTemplates = new HashSet<AccountActualTemplates>();
            AccountCategoryTemplates = new HashSet<AccountCategoryTemplates>();
            AccountTemplates = new HashSet<AccountTemplates>();
            Accounts = new HashSet<Accounts>();
            BudgetCategoryTemplates = new HashSet<BudgetCategoryTemplates>();
            BudgetItemTemplates = new HashSet<BudgetItemTemplates>();
            UserSettings = new HashSet<UserSettings>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public short PasswordVersion { get; set; }
        public bool IsLocked { get; set; }
        public bool IsEnabled { get; set; }
        public Guid? ReenableToken { get; set; }
        public Guid? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenCreatedOn { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Guid? LastLoginToken { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }
        public DateTime? LastLockoutDate { get; set; }
        public short FailedPasswordAttemptCount { get; set; }
        public string Notes { get; set; }
        public long GroupAssociation { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] Ts { get; set; }

        public ICollection<AccountActualTemplates> AccountActualTemplates { get; set; }
        public ICollection<AccountCategoryTemplates> AccountCategoryTemplates { get; set; }
        public ICollection<AccountTemplates> AccountTemplates { get; set; }
        public ICollection<Accounts> Accounts { get; set; }
        public ICollection<BudgetCategoryTemplates> BudgetCategoryTemplates { get; set; }
        public ICollection<BudgetItemTemplates> BudgetItemTemplates { get; set; }
        public ICollection<UserSettings> UserSettings { get; set; }
    }
}
