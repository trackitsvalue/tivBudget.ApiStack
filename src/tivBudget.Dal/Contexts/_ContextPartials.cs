using Microsoft.EntityFrameworkCore;
using freebyTech.Common.ExtensionMethods;

namespace tivBudget.Dal.Models
{


  public partial class freebyTrackContext : DbContext
  {
    partial void OnModelCreatingExt(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyEditableModelConfigurations();
    }
  }
}
