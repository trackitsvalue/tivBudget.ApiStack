using System;

namespace tivBudget.Dal.Models
{
  public class AccountAndCategoryMetadata
  {
    public Guid Id { get; set; }

    public DateTime oldestRelevantOn { get; set; }
    public DateTime newestRelevantOn { get; set; }
  }
}