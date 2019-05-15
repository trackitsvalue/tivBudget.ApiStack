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
  }
}
