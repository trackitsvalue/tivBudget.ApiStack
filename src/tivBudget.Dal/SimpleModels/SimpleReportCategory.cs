using System.Collections.Generic;

namespace tivBudget.Dal.SimpleModels
{
  public partial class SimpleReportCategory
  {
    public SimpleReportCategory()
    {
      Reports = new HashSet<SimpleReport>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int DisplayIndex { get; set; }

    public ICollection<SimpleReport> Reports { get; set; }
  }
}
