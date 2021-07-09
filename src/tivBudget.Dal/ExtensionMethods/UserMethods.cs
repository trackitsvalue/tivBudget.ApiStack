using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.VirtualModels;

namespace tivBudget.Dal.ExtensionMethods
{
  public static class UserMethods
  {

    /// Returns (and optionally creates if necessary) a setting identitifed by the passed setting name.
    public static UserSetting GetOrCreateUserSetting(this User user, string settingName, string defaultValue, string createdBy, DateTime createdOn)
    {
      var userSetting = user.UserSettings.FirstOrDefault((us) => us.Name == settingName);

      if (userSetting == null)
      {
        userSetting = new UserSetting()
        {
          IsNew = true,
          UserId = user.Id,
          ApplicationId = 0,
          Name = settingName,
          Value = defaultValue,
          CreatedBy = createdBy,
          CreatedOn = createdOn
        };
        user.UserSettings.Add(userSetting);
      }
      return (userSetting);
    }

    public static UserAccomplishmentModel ToModel(this UserAccomplishment userAccomplishment)
    {
      return new UserAccomplishmentModel()
      {
        Type = userAccomplishment.Type,
        SubType = userAccomplishment.SubType,
        Title = userAccomplishment.Title,
        Description = userAccomplishment.Description,
        Icon = userAccomplishment.Icon,
        IsAcknowledged = userAccomplishment.IsAcknowledged,
        CreatedOn = userAccomplishment.CreatedOn
      };
    }

    /// Extension method to add a new accomplishment to the user from a system accomplishment.
    public static void AddAccomplishment(this User user, SystemAccomplishmentModel systemAccomplishment, string createdBy, DateTime createdOn)
    {
      user.UserAccomplishments.Add(new UserAccomplishment()
      {
        IsNew = true,
        UserId = user.Id,
        ApplicationId = 0,
        Type = systemAccomplishment.Type,
        SubType = systemAccomplishment.SubType,
        Icon = systemAccomplishment.Icon,
        Title = systemAccomplishment.Title,
        Description = systemAccomplishment.Description,
        IsAcknowledged = false,
        CreatedBy = createdBy,
        CreatedOn = createdOn
      });
    }

    /// Checks if the user already has a certain accomplishment.
    public static bool HasAccomplishment(this User user, string type, string subType)
    {
      return (user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == type && ua.SubType == subType) != null);
    }
  }
}