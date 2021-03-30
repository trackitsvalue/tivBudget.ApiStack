using System;
using tivBudget.Dal.Models;

namespace tivBudget.Api.ExtensionMethods
{
  // TODO - If copyActuals is ever actually used, needs to be reexamined since credit card usage and also since default account linking are now things.
  public static class BudgetModelMethods
  {
    public static Guid CREDIT_CATEGORY_TEMPLATE_ID = new Guid("2dc7423e-f36b-1410-859a-008327386cdd");

    public static void CopyFinancialsToDestinationBudget(this Budget sourceBudget, Budget destinationBudget, bool copyActuals, bool overrideAreBudgetItemsOpen)
    {
      destinationBudget.EstimatedIncome += sourceBudget.EstimatedIncome;
      destinationBudget.EstimatedSpending += sourceBudget.EstimatedSpending;
      destinationBudget.EstimatedRemaining += sourceBudget.EstimatedRemaining;
      if (copyActuals)
      {
        destinationBudget.ActualIncome += sourceBudget.ActualIncome;
        destinationBudget.ActualMinusEstimatedIncome += sourceBudget.ActualMinusEstimatedIncome;
        destinationBudget.ActualSpending += sourceBudget.ActualSpending;
        destinationBudget.ActualRemaining += sourceBudget.ActualRemaining;
      }

      var inc = destinationBudget.BudgetCategories.Count;
      foreach (var category in sourceBudget.BudgetCategories)
      {
        // Do not copy revolving credit category when not copying actuals.
        if (!copyActuals && category.CategoryTemplateId.CompareTo(CREDIT_CATEGORY_TEMPLATE_ID) == 0)
        {

        }
        else
        {
          destinationBudget.BudgetCategories.Add(category.ToNewEntity(inc, copyActuals, overrideAreBudgetItemsOpen));
          inc++;
        }

      }
    }

    public static BudgetCategory ToNewEntity(this BudgetCategory category, int displayIndex, bool copyActuals, bool overrideAreBudgetItemsOpen)
    {
      var newCategoryEntity = new BudgetCategory();
      newCategoryEntity.Id = Guid.NewGuid();
      newCategoryEntity.IsNew = true;
      category.Copy(newCategoryEntity, displayIndex, copyActuals);
      if (overrideAreBudgetItemsOpen)
      {
        newCategoryEntity.AreBudgetItemsOpen = true;
      }
      var inc = 0;
      foreach (var item in category.BudgetItems)
      {
        newCategoryEntity.BudgetItems.Add(item.ToNewEntity(inc, copyActuals));
        inc++;
      }
      return (newCategoryEntity);
    }

    public static BudgetItem ToNewEntity(this BudgetItem item, int displayIndex, bool copyActuals)
    {
      var newItemEntity = new BudgetItem();
      newItemEntity.Id = Guid.NewGuid();
      newItemEntity.IsNew = true;
      item.Copy(newItemEntity, displayIndex, copyActuals);
      if (copyActuals)
      {
        var inc = 0;
        foreach (var actual in item.BudgetActuals)
        {
          newItemEntity.BudgetActuals.Add(actual.ToNewEntity(inc));
          inc++;
        }
      }
      return (newItemEntity);
    }

    public static BudgetActual ToNewEntity(this BudgetActual actual, int displayIndex)
    {
      var newActualEntity = new BudgetActual();
      newActualEntity.Id = Guid.NewGuid();
      newActualEntity.IsNew = true;
      actual.Copy(newActualEntity, displayIndex);
      return (newActualEntity);
    }

    //public static void Copy(this BudgetEntity sourceBudget, BudgetEntity destinationBudgetEntity, int displayIndex)
    //{
    //  destinationBudgetEntity.Description = sourceBudget.Description;
    //  destinationBudgetEntity.DisplayIndex = displayIndex;
    //  destinationBudgetEntity.Month = sourceBudget.Month;
    //  destinationBudgetEntity.Year = sourceBudget.Year;
    //}

    public static void Copy(this BudgetCategory sourceCategory, BudgetCategory destinationCategoryEntity, int displayIndex, bool copyActuals)
    {
      destinationCategoryEntity.AreBudgetItemsOpen = sourceCategory.AreBudgetItemsOpen;
      destinationCategoryEntity.CategoryTemplateId = sourceCategory.CategoryTemplateId;
      destinationCategoryEntity.Description = sourceCategory.Description;
      destinationCategoryEntity.DisplayIndex = displayIndex;
      destinationCategoryEntity.CategoryBudgeted = sourceCategory.CategoryBudgeted;
      if (copyActuals)
      {
        destinationCategoryEntity.CategorySpent = sourceCategory.CategorySpent;
        destinationCategoryEntity.CategoryRemaining = sourceCategory.CategoryRemaining;
        destinationCategoryEntity.CategorySpentByRevolvingCredit = sourceCategory.CategorySpentByRevolvingCredit;
      }
      else
      {
        destinationCategoryEntity.CategoryRemaining = sourceCategory.CategoryBudgeted;
      }
    }

    public static void Copy(this BudgetItem sourceItem, BudgetItem destinationItemEntity, int displayIndex, bool copyActuals)
    {
      destinationItemEntity.AmountBudgeted = sourceItem.AmountBudgeted;
      destinationItemEntity.AreBudgetActualsOpen = sourceItem.AreBudgetActualsOpen;
      destinationItemEntity.Description = sourceItem.Description;
      destinationItemEntity.DisplayIndex = displayIndex;
      destinationItemEntity.ItemTemplateId = sourceItem.ItemTemplateId;
      destinationItemEntity.IsLinked = sourceItem.IsLinked;
      destinationItemEntity.AccountLinkId = sourceItem.AccountLinkId;
      destinationItemEntity.AccountCategoryLinkId = sourceItem.AccountCategoryLinkId;
      if (copyActuals)
      {
        destinationItemEntity.ItemSpent = sourceItem.ItemSpent;
        destinationItemEntity.ItemRemaining = sourceItem.ItemRemaining;
        destinationItemEntity.ItemSpentByRevolvingCredit = sourceItem.ItemSpentByRevolvingCredit;
      }
      else
      {
        destinationItemEntity.ItemRemaining = sourceItem.AmountBudgeted;
      }
      destinationItemEntity.RecurringSettingsId = sourceItem.RecurringSettingsId;
    }

    public static void Copy(this BudgetActual sourceActual, BudgetActual destinationActualEntity, int displayIndex)
    {
      destinationActualEntity.Amount = sourceActual.Amount;
      destinationActualEntity.Description = sourceActual.Description;
      destinationActualEntity.DisplayIndex = displayIndex;
      destinationActualEntity.RelevantOn = sourceActual.RelevantOn;
      destinationActualEntity.IsLinked = sourceActual.IsLinked;
      destinationActualEntity.IsEnvelopeDeposit = sourceActual.IsEnvelopeDeposit;
    }
  }
}