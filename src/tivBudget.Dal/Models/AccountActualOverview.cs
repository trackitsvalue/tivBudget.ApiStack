using System;
using freebyTech.Common.Data.Interfaces;

namespace tivBudget.Dal.Models
{
  public partial class AccountActualOverview : IEditableModel
  {
    public Guid Id { get; set; }
    public Guid ActualTemplateId { get; set; }
    public Guid? BudgetActualLinkId { get; set; }
    public Guid CategoryId { get; set; }
    public string Description { get; set; }
    public DateTime RelevantOn { get; set; }
    public decimal Amount { get; set; }
    public bool IsLinked { get; set; }
    public bool IsRecurring { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }
    public byte[] Ts { get; set; }
    public bool IsBudgetDefaultLink { get; set; }
    public AccountActualTemplate ActualTemplate { get; set; }
    public BudgetActual BudgetActualLink { get; set; }

    #region Non-Model Helper Properties

    public bool IsNew { get; set; }
    public bool IsDirty { get; set; }
    public bool IsDeleted { get; set; }
    public Decimal EndingBalance { get; set; }
    public bool IsFound { get; set; } // <-- Used in updates
    public bool IsSummationLine { get; set; } // <-- Inserted line saying what last period's ending value was. This line is never saved to the DB.

    #endregion
  }
}
