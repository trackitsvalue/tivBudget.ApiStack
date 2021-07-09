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

    /// Updates the current user to the passed user level if necessary.
    public static void UpdateUserLevelIfChanged(User user, int newLevel)
    {
      var levelSetting = user.GetOrCreateUserSetting(UserSettingTypes.LevelSetting, "0", user.CreatedBy, user.CreatedOn);

      if (int.Parse(levelSetting.Value) < newLevel)
      {
        levelSetting.Value = newLevel.ToString();
        if (!levelSetting.IsNew)
        {
          levelSetting.IsDirty = true;
        }
      }
    }

    /// Returns the current user experience points.
    public static int GetUserExperience(User user)
    {
      var experienceSetting = user.GetOrCreateUserSetting(UserSettingTypes.ExperienceSetting, "0", user.CreatedBy, user.CreatedOn);
      return int.Parse(experienceSetting.Value);
    }

    /// Updates the user's experience points and returns the new value.
    public static int AddUserExperience(User user, int experienceToAdd)
    {
      var experienceSetting = user.GetOrCreateUserSetting(UserSettingTypes.ExperienceSetting, "0", user.CreatedBy, user.CreatedOn);
      var newExperience = (experienceToAdd + int.Parse(experienceSetting.Value));
      experienceSetting.Value = newExperience.ToString();
      if (!experienceSetting.IsNew)
      {
        experienceSetting.IsDirty = true;
      }
      return newExperience;
    }
  }
}