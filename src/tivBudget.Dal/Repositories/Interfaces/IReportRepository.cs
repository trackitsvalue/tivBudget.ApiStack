using System.Collections.Generic;
using tivBudget.Dal.SimpleModels;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IReportRepository
  {
    List<SimpleReportCategory> FindAllReports();
  }
}