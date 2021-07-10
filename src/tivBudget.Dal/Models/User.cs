using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class User : IEditableModel
  {
    public User()
    {
      AccountActualTemplates = new HashSet<AccountActualTemplate>();
      AccountCategoryTemplates = new HashSet<AccountCategoryTemplate>();
      AccountTemplates = new HashSet<AccountTemplate>();
      Accounts = new HashSet<Account>();
      BudgetCategoryTemplates = new HashSet<BudgetCategoryTemplate>();
      BudgetItemTemplates = new HashSet<BudgetItemTemplate>();
      News = new HashSet<News>();
      UserAccomplishments = new HashSet<UserAccomplishment>();
      UserSettings = new HashSet<UserSetting>();
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

    public ICollection<AccountActualTemplate> AccountActualTemplates { get; set; }
    public ICollection<AccountCategoryTemplate> AccountCategoryTemplates { get; set; }
    public ICollection<AccountTemplate> AccountTemplates { get; set; }
    public ICollection<Account> Accounts { get; set; }
    public ICollection<BudgetCategoryTemplate> BudgetCategoryTemplates { get; set; }
    public ICollection<BudgetItemTemplate> BudgetItemTemplates { get; set; }
    public ICollection<News> News { get; set; }
    public ICollection<UserAccomplishment> UserAccomplishments { get; set; }
    public ICollection<UserSetting> UserSettings { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
