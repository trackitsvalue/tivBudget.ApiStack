using System;

namespace tivBudget.Dal.SimpleModels
{
  public class SimpleNews
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public Guid? OwnerId { get; set; }
    public int ItemType { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedOn { get; set; }
  }
}
