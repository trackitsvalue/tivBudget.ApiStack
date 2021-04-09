using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class VideoCategory : IEditableModel
  {
    public VideoCategory()
    {
      Videos = new HashSet<Video>();
    }

    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    public ICollection<Video> Videos { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
