using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class Quote : IEditableModel
  {
    public int Id { get; set; }
    public string Source { get; set; }
    public string Text { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    #endregion
  }
}
