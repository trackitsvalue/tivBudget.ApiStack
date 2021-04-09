using System;
using System.Collections.Generic;

namespace tivBudget.Dal.SimpleModels
{
  public partial class SimpleVideoCategory
  {
    public SimpleVideoCategory()
    {
      Videos = new HashSet<SimpleVideo>();
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public ICollection<SimpleVideo> Videos { get; set; }
  }
}
