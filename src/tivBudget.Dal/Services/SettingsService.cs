using System;
using System.Linq;
using tivBudget.Dal.ExtensionMethods;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Services
{
  public class UserSettingTypes
  {
    public const string LevelSetting = "Level";
    public const string ExperienceSetting = "Experience";
  }

  public class SettingsService
  {
    /// Returns the current user level.
    public static int GetUserLevel(User user)
    {
      var levelSetting = user.GetOrCreateUserSetting(UserSettingTypes.LevelSetting, "0", user.CreatedBy, user.CreatedOn);
      return int.Parse(levelSetting.Value);
    }

    /// Updates the current user to the passed user level if necessary and returns true when an update occurred.
    public static bool UpdateUserLevelIfNecessary(User user, int newLevel)
    {
      var levelSetting = user.GetOrCreateUserSetting(UserSettingTypes.LevelSetting, "0", user.CreatedBy, user.CreatedOn);

      if (int.Parse(levelSetting.Value) < newLevel)
      {
        levelSetting.Value = newLevel.ToString();
        if (!levelSetting.IsNew)
        {
          levelSetting.IsDirty = true;
        }
        return true;
      }
      return false;
    }

    /// Returns the current user experience points.
    public static int GetUserExperience(User user)
    {
      var experienceSetting = user.GetOrCreateUserSetting(UserSettingTypes.ExperienceSetting, "0", user.CreatedBy, user.CreatedOn);
      return int.Parse(experienceSetting.Value);
    }

    /// Updates the user's experience points if necessary and returns true when an update was needed.
    public static bool UpdateUserExperienceIfNecessary(User user)
    {
      var experienceSetting = user.GetOrCreateUserSetting(UserSettingTypes.ExperienceSetting, "0", user.CreatedBy, user.CreatedOn);

      var newExperienceSummation = user.UserAccomplishments.Sum((ua) => ua.EarnedExperience);

      if (int.Parse(experienceSetting.Value) != newExperienceSummation)
      {
        experienceSetting.Value = newExperienceSummation.ToString();
        if (!experienceSetting.IsNew)
        {
          experienceSetting.IsDirty = true;
        }
        return true;
      }
      return false;
    }
  }
}