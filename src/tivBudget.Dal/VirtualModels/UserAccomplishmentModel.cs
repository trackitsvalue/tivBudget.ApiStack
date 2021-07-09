using System;

namespace tivBudget.Dal.VirtualModels
{
  /// <summary>
  /// The model representing the current status of the user.
  /// </summary>
  public class UserAccomplishmentModel
  {
    public string Type { get; set; }
    public string SubType { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public bool IsAcknowledged { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
