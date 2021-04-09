using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class Video : IEditableModel
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string VideoEmbed { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    public VideoCategory Category { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
