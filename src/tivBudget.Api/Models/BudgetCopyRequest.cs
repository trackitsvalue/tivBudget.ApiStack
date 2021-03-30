using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Api.Models
{
  public partial class BudgetCopyRequest
  {
    /// <summary> The Id property of the Source BudgetResponse</summary>
    public Guid SourceId { get; set; }

    /// <summary> The Description property for the Destination BudgetResponse</summary>
    public virtual string DestinationDescription { get; set; }

    /// <summary> The Month property for the Destination BudgetResponse</summary>
    public int DestinationMonth { get; set; }

    /// <summary> The Year property for the Destination BudgetResponse</summary>
    public int DestinationYear { get; set; }

    /// <summary> Whether or not Actuals should also be copied into the Destination BudgetResponse</summary>
    public bool CopyActuals { get; set; }
  }
}
