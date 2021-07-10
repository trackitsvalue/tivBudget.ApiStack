using System;

namespace tivBudget.Dal.VirtualModels
{
  /// <summary>
  /// The model representing the current status of the user.
  /// </summary>
  public class UserSettingModel
  {
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime CreatedOn { get; set; }
  }
}
