
namespace tivBudget.Dal.VirtualModels
{
  /// Accomplishment information as it is defined in the system
  public class SystemAccomplishmentModel
  {
    /// Type of the accomplishment as it is known in the system
    public string Type { get; set; }
    /// The subtype of the accomplishment as it is known in the system
    public string SubType { get; set; }
    /// Title of the accomplishment as it is known in the system
    public string Title { get; set; }
    /// A description for the accommplishment in english when referenced in the system if different from user description, otherwise not set.
    public string GeneralDescription { get; set; }
    /// A description for the accommplishment in english when applied to the user
    public string Description { get; set; }
    /// The icon to display for the accomplishment
    public string Icon { get; set; }
    /// The experience given (for Experience Accomplishments) or the experience needed (for Level Accomplishments)
    public int Experience { get; set; }
    /// The level at which this privilege is earned or the Level number for Level Accomplishments
    public int Level { get; set; }
  }
}