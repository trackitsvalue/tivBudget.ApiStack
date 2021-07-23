using System;
using System.Collections.Generic;
using System.Linq;
using tivBudget.Dal.ExtensionMethods;
using tivBudget.Dal.Models;
using tivBudget.Dal.VirtualModels;

namespace tivBudget.Dal.Services
{
  /// The Accomplishment types that exist within the system.
  public class UserAccomplishmentTypes
  {
    /// Type ID for the level type accomplishment
    public const string LevelAccomplishmentId = "level";
    /// Type for the level type accomplishment
    public const string LevelAccomplishment = "Level Up Accomplished";
    /// Icon for the level type accomplishment
    public const string LevelAccomplishmentIcon = "icofont-shield-alt";
    /// Type ID for the experience type accomplishment
    public const string ExperienceAccomplishmentId = "experience";
    /// Type for the experience type accomplishment
    public const string ExperienceAccomplishment = "Experience Gained";
    /// Icon for the experience type accomplishment
    public const string ExperienceAccomplishmentIcon = "icofont-dart";
    /// Type ID for the privilege type accomplishment
    public const string PrivilegeAccomplishmentId = "privilege";
    /// Type for the privilege type accomplishment
    public const string PrivilegeAccomplishment = "Ability Added";

    /// Icon for the privilege type accomplishment
    public const string PrivilegeAccomplishmentIcon = "icofont-fix-tools";
  }

  /// Enum representing the experience types
  public enum ExperienceTypeIds : int
  {
    /// The user has been created.
    UserCreated,
    /// User has created their first budget.
    FirstBudgetCreated,
    /// User has created their basic accounts.
    BasicAccountsCreated,
    /// A New Budget has been created besides the first budget and was done in a timely manner
    BudgetCreatedEarlybird,
    /// A New Budget has been created besides the first budget
    BudgetCreated,
    /// A New Acount has been created besides basic accounts
    AccountCreated,
    /// A Budget has been completed
    BudgetCompleted,
    /// A Budget has been completed and saved money
    BudgetCompletedSavedMoney,
    /// A Budget has been completed and cc usage was paid off.
    BudgetCompletedCreditPaidOff,
    /// A Budget has been completed and some debt was paid down.
    BudgetCompletedDebtPaidDown,
    /// A Budget has been completed and charity was given.
    BudgetCompletedCharitable,
    /// A Budget has been completed and half tithing was given.
    BudgetCompletedHalfTithing,
    /// A Budget has been completed and tithing was given.
    BudgetCompletedTithing,
    /// A snowball account has been paid off.
    // SnowballAccountPaidOff
  };

  /// The Accomplishment Service
  public class AccomplishmentService
  {
    /// The experience types that exist in the system.
    public static List<SystemAccomplishmentModel> ExperienceTypes = new List<SystemAccomplishmentModel>() {
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "user-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - User Created", GeneralDescription="The user created their account.", Description = "The first step is done, your user is created.", Experience = 50, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "first-budget-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - First Budget Created", GeneralDescription = "The user's first budget has been created.", Description = "Your first budget has been created for {0}/{1} (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "basic-acccounts-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Basic Accounts Created", GeneralDescription = "The user's basic bank and credit accounts have been created.", Description = "Your basic bank and credit accounts have been created (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-created-earlybird", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Earlybird Budget Created", GeneralDescription = "The user created a new budget BEFORE A MONTH STARTED.", Description = "Budget created for {0}/{1} (50 experience points).", Experience = 50, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Created", GeneralDescription = "The user created a new budget WITHIN FIVE DAYS OF THE MONTH STARTING.", Description = "Budget created for {0}/{1} (25 experience points).", Experience = 25, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "account-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Account Created", GeneralDescription = "The user created a new account.", Description = "Account {0} created (20 experience points).", Experience = 20, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed", GeneralDescription = "The user's budget has been zeroed and closed WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} is now fully zeroed! It has been completed (50 experience points).", Experience = 50, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-saved-money", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Savings", GeneralDescription = "The user's budget has been zeroed and closed withat least $100 dollars in savings WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed and saved money (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-credit-paid-off", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Credit Cards Paid Off", GeneralDescription = "The user's budget has been zeroed and closed with all used credit at least fully paid off WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed and credit cards were paid off or better (50 experience points).", Experience = 50, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-debt-paid-down", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Debt Paid Down", GeneralDescription = "The user's budget has been zeroed and closed and at least $100 dollars in long term debt paid down WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed and debt was paid down (75 experience points).", Experience = 75, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-charitable", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Charitable Giving", GeneralDescription = "The user's budget has been zeroed and closed and at least $100 dollars in charity was given WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed with charitable giving (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-half-tithing", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Half Tithing", GeneralDescription = "The user's budget has been zeroed and closed and at least 5% of income was given in charity WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed with at least 5% of income given in charitable giving (150 experience points).", Experience = 150, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-tithing", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Tithing", GeneralDescription = "The user's budget has been zeroed and closed and at least 10% of income was given in charity WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed with at least 10% of income given in charitable giving (200 experience points).", Experience = 200, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      // new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-completed-debt-snowball-debt-paid-off", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Completed With Debt Snowball Paid Down", GeneralDescription = "The user's budget has been zeroed and closed and paid off at least $100 dollars in long term debt from the debt snowball WITHIN 15 DAYS AFTER THE END OF THE MONTH AND AT MOST 2 DAYS BEFORE THE END OF THE MONTH.", Description = "Budget from {0}/{1} completed and Debt Snowball debt was paid down (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      // new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "snowball-accoount-paid-off", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Debt Snowball Account Paid Off", GeneralDescription = "An account in the user's Debt Snowball has been paid off. This is a 1 time experience gain per account.", Description = "The account {0} in your Debt Snowball has been paid off!", Experience = 200, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
    };

    /// Accomplishment Levels that exist in the system.
    public static List<SystemAccomplishmentModel> AccomplishmentLevels = new List<SystemAccomplishmentModel>() {
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.LevelAccomplishmentId, SubType = "level-1", Title = $"{UserAccomplishmentTypes.LevelAccomplishment} - Level 1", Description = "Level 1 gives you the ability to create budgets and to tie them to primary bank and credit accounts.", Level = 1, Experience = 50, Icon = UserAccomplishmentTypes.LevelAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.LevelAccomplishmentId, SubType = "level-2", Title = $"{UserAccomplishmentTypes.LevelAccomplishment} - Level 2", Description = "Level 2 gives you greater flexibility to create new account types and tie them to budgets.", Level = 2, Experience = 200, Icon = UserAccomplishmentTypes.LevelAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.LevelAccomplishmentId, SubType = "level-3", Title = $"{UserAccomplishmentTypes.LevelAccomplishment} - Level 3", Description = "Level 3 gives you insight into your total wealth picture with new dashboards and reports.", Level = 3, Experience = 5000, Icon = UserAccomplishmentTypes.LevelAccomplishmentIcon }
    };

    /// The system privileges that exist and at what levels.
    public static List<SystemAccomplishmentModel> PrivilegesAtLevels = new List<SystemAccomplishmentModel>() {
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-management-level-1", Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Create Budgets (Level 1)", Description = "User can create new budgets, copy existing budgets, use the basic features of the budget wizard.", Level = 1, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "account-management-level-1",Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Create Accounts (Level 1)", Description = "User can create the primary bank and credit card accounts. All budget items are tied to that primary bank account and all credit card items are tied to that primary credit card.", Level = 1, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon  },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-management-level-2",Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Budget Management (Level 2)", Description = "User can tie non-primary accounts to budgets for purposes of transferring money, paying off debt, saving to retirement accounts, etc.", Level = 2, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "account-management-level-2",Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Accounts Management (Level 2)", Description = "User can create new accounts of all account types like bank accounts, mortgages, credit cards, retirement accounts, etc. User can also hide inactive accounts.", Level = 2, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon  },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-dashboards-level-3",Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Dashboards (Level 3)", Description = "User can use dashboards which allow them to see various aspects of their financial situation.", Level = 3, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-reports-level-3", Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Reports (Level 3)", Description = "User can run reports against their budget and account data.", Level = 3, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon  }
    };

    // General Accomplishment Methods
    //-------------------------------

    /// Retrieves all user settings and accomplishments and metadata about user and accomplishments,
    /// can also optionally acknowledge all accomplishments before it returns.
    public static bool GetAccomplishmentsAndSettings(UserStatusModel statusModel, User user, bool acknowledgeAccomplishments)
    {
      var saveToDb = false;
      var userAccomplishments = new List<UserAccomplishmentModel>();

      if (acknowledgeAccomplishments)
      {
        var accomplishmentsToAcknowledge = user.UserAccomplishments.Where((ua) => !ua.IsAcknowledged).ToList();
        if (accomplishmentsToAcknowledge.Count > 0)
        {
          foreach (var userAccomplishment in accomplishmentsToAcknowledge)
          {
            userAccomplishment.IsAcknowledged = true;
            userAccomplishment.IsDirty = true;
          }
          saveToDb = true;
        }
      }

      foreach (var userAccomplishment in user.UserAccomplishments.OrderBy(ua => ua.CreatedOn))
      {
        userAccomplishments.Add(userAccomplishment.ToModel());
      }
      statusModel.UserAccomplishments = userAccomplishments.ToArray();

      statusModel.HasNewExperience = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.ExperienceAccomplishmentId && !ua.IsAcknowledged) != null;
      statusModel.NewExperienceAmount = user.UserAccomplishments.Where((ua) => ua.Type == UserAccomplishmentTypes.ExperienceAccomplishmentId && !ua.IsAcknowledged).Sum((ua) => ua.EarnedExperience);
      statusModel.IsNewLevel = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.LevelAccomplishmentId && !ua.IsAcknowledged) != null;
      statusModel.HasNewPrivilege = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.PrivilegeAccomplishmentId && !ua.IsAcknowledged) != null;
      statusModel.NewPrivilegeCount = user.UserAccomplishments.Where((ua) => ua.Type == UserAccomplishmentTypes.PrivilegeAccomplishmentId && !ua.IsAcknowledged).Count();
      statusModel.HasNewAccomplishment = statusModel.HasNewExperience | statusModel.IsNewLevel | statusModel.HasNewPrivilege;
      statusModel.Experience = SettingsService.GetUserExperience(user);
      statusModel.Level = SettingsService.GetUserLevel(user);
      statusModel.UserLevelInfo = AccomplishmentService.GetUserLevelInfo(user);

      var userSettings = new List<UserSettingModel>();
      foreach (var userSetting in user.UserSettings.OrderBy(us => us.CreatedOn))
      {
        if (userSetting.Name != UserSettingTypes.LevelSetting && userSetting.Name != UserSettingTypes.ExperienceSetting)
        {
          userSettings.Add(new UserSettingModel()
          {
            Name = userSetting.Name,
            Value = userSetting.Value,
            CreatedOn = userSetting.CreatedOn
          });
        }
      }
      statusModel.UserSettings = userSettings.ToArray();

      return saveToDb;
    }

    /// Returns level information for a passed user level.
    public static UserLevelAccomplishmentsModel GetUserLevelInfo(User user)
    {
      var userLevel = SettingsService.GetUserLevel(user);
      var userLevelAccomplishments = new UserLevelAccomplishmentsModel();
      var existingAccomplishments = new List<UserLevelAccomplishmentModel>();
      // UI wants the info backwards
      for (int dec = userLevel; dec > 0; dec--)
      {
        var levelAccomplishmentInfo = GetLevelInfo(dec);
        var userLevelInfo = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.LevelAccomplishmentId && ua.SubType == $"level-{dec}");
        if (userLevelInfo != null)
        {
          levelAccomplishmentInfo.UserLevelAccomplishmentInfo = userLevelInfo.ToModel();
        }
        existingAccomplishments.Add(levelAccomplishmentInfo);
      }
      userLevelAccomplishments.AccomplishedLevels = existingAccomplishments.ToArray();
      userLevelAccomplishments.NextLevel = GetLevelInfo(userLevel + 1);
      return userLevelAccomplishments;
    }

    /// Returns the information for a given level.
    public static UserLevelAccomplishmentModel GetLevelInfo(int level)
    {
      UserLevelAccomplishmentModel levelAccomplishmentInfo = null;
      var levelInfo = AccomplishmentLevels.FirstOrDefault((al) => al.Level == level);

      if (levelInfo != null)
      {
        levelAccomplishmentInfo = new UserLevelAccomplishmentModel();
        levelAccomplishmentInfo.LevelInfo = levelInfo;
        levelAccomplishmentInfo.LevelPrivileges = PrivilegesAtLevels.FindAll((pl) => pl.Level == level).ToArray();
      }

      return levelAccomplishmentInfo;
    }

    /// Updates the user's experience level if necessary and checks for the need for a level update.
    /// Returns true if a change was required, otherwise returns false.
    public static bool ResetExperienceIfNecessaryAndCheckForLevelChange(User user, string createdBy, DateTime createdOn)
    {
      var dbSaveNeeded = false;
      var maxLevel = 0;

      // Was there an experience update?
      if (SettingsService.UpdateUserExperienceIfNecessary(user))
      {
        var currentExperience = SettingsService.GetUserExperience(user);

        var userAccomplishmentLevels = AccomplishmentLevels.FindAll((al) => al.Experience <= currentExperience);

        foreach (var userAccomplishmentLevel in userAccomplishmentLevels)
        {
          if (userAccomplishmentLevel.Level > maxLevel)
          {
            maxLevel = userAccomplishmentLevel.Level;
          }
          // If we don't already have this level then add it
          if (!user.HasAccomplishment(userAccomplishmentLevel.Type, userAccomplishmentLevel.SubType))
          {
            user.AddAccomplishment(userAccomplishmentLevel, Guid.Empty, 0, createdBy, createdOn);
            dbSaveNeeded = true;
          }

          var levelPrivileges = PrivilegesAtLevels.FindAll((pl) => pl.Level == userAccomplishmentLevel.Level);

          foreach (var levelPrivilege in levelPrivileges)
          {
            // If we don't already have this privilege then add it
            if (!user.HasAccomplishment(levelPrivilege.Type, levelPrivilege.SubType))
            {
              user.AddAccomplishment(levelPrivilege, Guid.Empty, 0, createdBy, createdOn);
              dbSaveNeeded = true;
            }
          }
        }
        if (SettingsService.UpdateUserLevelIfNecessary(user, maxLevel))
        {
          dbSaveNeeded = true;
        }
      }
      return dbSaveNeeded;
    }
  }
}