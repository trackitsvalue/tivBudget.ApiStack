using System;
using System.Collections.Generic;

namespace tivBudget.Dal.SimpleModels
{
  public partial class SimpleReport
  {
    public SimpleReport()
    {
      ReportControls = new HashSet<SimpleReportControl>();
    }

    public Guid Id { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public int DisplayIndex { get; set; }
    public string StoredProcedure { get; set; }
    public string ReportHelper { get; set; }
    public SimpleReportCategory Category { get; set; }
    public ICollection<SimpleReportControl> ReportControls { get; set; }
  }
}
