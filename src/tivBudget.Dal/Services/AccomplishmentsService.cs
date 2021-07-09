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
    /// A New Budget has been created besides the first budget
    BudgetCreated,
    /// A New Acount has been created besides basic accounts
    AccountCreated,
    /// A Budget has been deleted
    BudgetDeleted,

    /// A non-used Account has been deleted
    AccountDeleted,
  };

  /// The Accomplishment Service
  public class AccomplishmentService
  {
    /// The experience types that exist in the system.
    public static List<SystemAccomplishmentModel> ExperienceTypes = new List<SystemAccomplishmentModel>() {
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "user-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - User Created", Description = "The first step is done, your user is created (50 experience points).", Experience = 50, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "first-budget-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - First Budget Created", Description = "Your first budget has been created (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "basic-acccounts-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Basic Accounts Created", Description = "Your basic bank and credit accounts have been created (100 experience points).", Experience = 100, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Created", Description = "New budget created (20 experience points).", Experience = 20, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "account-created", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Account Created", Description = "New budget created (20 experience points).", Experience = 20, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "budget-deleted", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Budget Deleted", Description = "A budget has been deleted (-20 experience points).", Experience = -20, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.ExperienceAccomplishmentId, SubType = "account-deleted", Title = $"{UserAccomplishmentTypes.ExperienceAccomplishment} - Account Deleted", Description = "An unused account has been deleted (-20 experience points).", Experience = -20, Icon = UserAccomplishmentTypes.ExperienceAccomplishmentIcon },
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
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-management-level-2",Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Accounts Management (Level 2)", Description = "User can create new accounts of all account types like bank accounts, mortgages, credit cards, retirement accounts, etc. User can also hide inactive accounts.", Level = 2, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon  },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-dashboards-level-3",Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Dashboards (Level 3)", Description = "User can use dashboards which allow them to see various aspects of their financial situation.", Level = 3, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon },
      new SystemAccomplishmentModel() { Type = UserAccomplishmentTypes.PrivilegeAccomplishmentId, SubType = "budget-reports-level-3", Title = $"{UserAccomplishmentTypes.PrivilegeAccomplishment} - Reports (Level 3)", Description = "User can run reports against their budget and account data.", Level = 3, Icon = UserAccomplishmentTypes.PrivilegeAccomplishmentIcon  }
    };

    /// Returns level information for a passed user level.
    public static UserLevelAccomplishmentsModel GetUserLevelInfo(User user)
    {
      var userLevel = SettingsService.GetUserLevel(user);
      var userLevelAccomplishments = new UserLevelAccomplishmentsModel();
      var existingAccomplishments = new List<UserLevelAccomplishmentModel>();
      // UI wants the info backwards
      for (int dec = userLevel; dec > 0; dec--)
      {
        var levelAccomplishmentInfo = GetLevelInfo(userLevel);
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

    /// Updates the user's experience level and checks for the need for a level update.
    public static void AddExperienceAndCheckForLevelChange(User user, int newExperience, string createdBy, DateTime createdOn)
    {
      var maxLevel = 0;

      var currentExperience = SettingsService.AddUserExperience(user, newExperience);

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
          user.AddAccomplishment(userAccomplishmentLevel, createdBy, createdOn);
        }

        var levelPrivileges = PrivilegesAtLevels.FindAll((pl) => pl.Level == userAccomplishmentLevel.Level);

        foreach (var levelPrivilege in levelPrivileges)
        {
          // If we don't already have this privilege then add it
          if (!user.HasAccomplishment(levelPrivilege.Type, levelPrivilege.SubType))
          {
            user.AddAccomplishment(levelPrivilege, createdBy, createdOn);
          }
        }
      }

      SettingsService.UpdateUserLevelIfChanged(user, maxLevel);
    }
  }
}