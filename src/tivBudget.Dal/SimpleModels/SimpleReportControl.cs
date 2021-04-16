using System;

namespace tivBudget.Dal.SimpleModels
{
  public partial class SimpleReportControl
  {
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public int DisplayIndex { get; set; }
    public string Description { get; set; }
    public string Html { get; set; }
  }
}
