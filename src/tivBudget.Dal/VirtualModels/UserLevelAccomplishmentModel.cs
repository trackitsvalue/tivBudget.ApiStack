
namespace tivBudget.Dal.VirtualModels
{
  /// Accomplishment information as it is defined in the system for a give level.
  public class UserLevelAccomplishmentModel
  {
    /// If applicable, the User's level accomplishment info associated with achieving this level.
    public UserAccomplishmentModel UserLevelAccomplishmentInfo { get; set;}

    /// The level system level information.
    public SystemAccomplishmentModel LevelInfo { get; set; }

    /// The privileges associated with this level.
    public SystemAccomplishmentModel[] LevelPrivileges { get; set; }
  }
}