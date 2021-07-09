using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using freebyTech.Common.Web.Logging.Interfaces;
using tivBudget.Api.Models;
using tivBudget.Api.Services;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using tivBudget.Dal.Services;
using tivBudget.Dal.VirtualModels;
using tivBudget.Dal.ExtensionMethods;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Account API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  [Authorize]
  public class UserController : ControllerBase
  {
    private IBudgetRepository BudgetRepo { get; }
    private IAccountRepository AccountRepo { get; }
    private IUserRepository UserRepo { get; }

    private IApiRequestLogger RequestLogger { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountRepository">Repo to use for account information.</param>
    /// <param name="budgetRepository">Repo to use for budget information.</param>
    /// <param name="userRepository">Repo to user for user information.</param>
    /// <param name="requestLogger">Logger used to log information about request.</param>
    public UserController(IAccountRepository accountRepository, IBudgetRepository budgetRepository, IUserRepository userRepository, IApiRequestLogger requestLogger)
    {
      RequestLogger = requestLogger;
      UserRepo = userRepository;
      AccountRepo = accountRepository;
      BudgetRepo = budgetRepository;
    }

    /// <summary>
    /// Returns the status of the user.
    /// </summary>
    [HttpGet("status")]
    public IActionResult GetStatus()
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      RequestLogger.UserId = userFromAuth.Id.ToString();

      var statusModel = new UserStatusModel()
      {
        IsEnabled = userFromAuth.IsEnabled,
        AccountCount = AccountRepo.FindCountByOwner(userFromAuth.Id),
        BudgetCount = BudgetRepo.FindCountByOwner(userFromAuth.Id),
      };
      if (statusModel.AccountCount == 0 || statusModel.BudgetCount == 0)
      {
        statusModel.IsNew = true;
      }
      GetAccomplishmentsAndSettings(statusModel, userFromAuth);
      return Ok(statusModel);
    }

    /// <summary>
    /// Returns the timeline of the user.
    /// </summary>
    [HttpGet("timeline")]
    public IActionResult GetTimeline()
    {
      var userFromAuth = UserService.GetUserFromClaims(this.User, UserRepo, RequestLogger);

      var userTimeline = new List<TimelineSection>();
      var thisYear = DateTime.Now.Year;
      var thisMonth = DateTime.Now.ToString("MMMM");
      var lastYear = -1;
      var lastMonth = "";
      TimelineSection lastUserTimelineSection = null;

      foreach (var userAccomplishment in userFromAuth.UserAccomplishments.OrderByDescending((ua) => ua.CreatedOn))
      {
        var currentYear = userAccomplishment.CreatedOn.Year;
        var currentMonth = userAccomplishment.CreatedOn.ToString("MMMM");

        if (currentYear != lastYear || currentMonth != lastMonth)
        {
          lastYear = currentYear;
          lastMonth = currentMonth;
          lastUserTimelineSection = new TimelineSection() { SectionLabel = $"{currentMonth} of  {currentYear.ToString()}" };
          userTimeline.Add(lastUserTimelineSection);
        }

        lastUserTimelineSection.SectionData.Add(new TimelineItem()
        {
          Date = userAccomplishment.CreatedOn.ToString("dddd, MMMM dd"),
          Title = userAccomplishment.Title,
          Content = userAccomplishment.Description,
          Icon = userAccomplishment.Icon,
        });
      }
      return Ok(userTimeline);
    }

    private void GetAccomplishmentsAndSettings(UserStatusModel statusModel, User user)
    {
      var userAccomplishments = new List<UserAccomplishmentModel>();
      foreach (var userAccomplishment in user.UserAccomplishments.OrderBy(ua => ua.CreatedOn))
      {
        userAccomplishments.Add(userAccomplishment.ToModel());
      }
      statusModel.UserAccomplishments = userAccomplishments.ToArray();

      statusModel.HasNewExperience = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.ExperienceAccomplishmentId && !ua.IsAcknowledged) != null;
      statusModel.IsNewLevel = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.LevelAccomplishmentId && !ua.IsAcknowledged) != null;
      statusModel.HasNewPrivilege = user.UserAccomplishments.FirstOrDefault((ua) => ua.Type == UserAccomplishmentTypes.PrivilegeAccomplishmentId && !ua.IsAcknowledged) != null;
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

    }
  }
}