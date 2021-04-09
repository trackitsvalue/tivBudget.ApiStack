using System;

namespace tivBudget.Dal.SimpleModels
{
  public partial class SimpleVideo
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string VideoEmbed { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
  }
}
