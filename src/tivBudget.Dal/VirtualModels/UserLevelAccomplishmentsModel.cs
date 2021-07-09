
namespace tivBudget.Dal.VirtualModels
{
  /// Accomplishment information as it is defined in the system for the current user
  public class UserLevelAccomplishmentsModel
  {
    // Levels already acheived by this user.
    public UserLevelAccomplishmentModel[] AccomplishedLevels { get; set; }

    // Next level information for this user.
    public UserLevelAccomplishmentModel NextLevel { get; set; }
  }
}