using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class Report : IEditableModel
  {
    public Report()
    {
      ReportControls = new HashSet<ReportControl>();
    }

    public Guid Id { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public int DisplayIndex { get; set; }
    public string StoredProcedure { get; set; }
    public string ReportHelper { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    public ReportCategory Category { get; set; }
    public ICollection<ReportControl> ReportControls { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
