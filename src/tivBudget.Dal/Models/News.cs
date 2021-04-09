using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class News : IEditableModel
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public Guid? OwnerId { get; set; }
    public int ItemType { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    public User Owner { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
