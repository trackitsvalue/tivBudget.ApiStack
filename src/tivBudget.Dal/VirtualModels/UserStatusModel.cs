namespace tivBudget.Dal.VirtualModels
{
  /// <summary>
  /// The model representing the current status of the user.
  /// </summary>
  public class UserStatusModel
  {
    /// <summary>
    /// Whether or not the user is currently enabled within the system.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Whether or not the user is considered new.
    /// </summary>
    public bool IsNew { get; set; }

    /// <summary>
    /// The total count of budgets the user owns or contributes to.
    /// </summary>
    public int BudgetCount { get; set; }

    /// <summary>
    /// The total count of accounts the user owns or contributes to.
    /// </summary>
    public int AccountCount { get; set; }

    /// The level of the user.
    public int Level { get; set; }

    /// The experience points of the user.
    public int Experience { get; set; }

    /// Whether or not the user has a new unacknowledged accomplishment.
    public bool HasNewAccomplishment { get; set; }

    /// Whether or not the user has has an unacknowledged level accomplishment.
    public bool IsNewLevel { get; set; }

    /// Whether or not the user has an unacknowledged experience accomplishment.
    public bool HasNewExperience { get; set; }

    /// Whether or not the user has an unacknowledged privilege accomplishment.
    public bool HasNewPrivilege { get; set; }

    public UserLevelAccomplishmentsModel UserLevelInfo { get; set; }

    public UserAccomplishmentModel[] UserAccomplishments { get; set; }

    public UserSettingModel[] UserSettings { get; set; }
  }
}
