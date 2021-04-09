using System;
using System.Collections.Generic;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class PageContent : IEditableModel
  {
    public Guid Id { get; set; }
    public string PageName { get; set; }
    public string PageSection { get; set; }
    public string HtmlContent { get; set; }
    public int Version { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }

    #endregion
  }
}
