using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Api.Models
{
  public class BudgetDashboardRequest
  {
    public string StartDate { get; set; }

    public string EndDate { get; set; }
  }
}
