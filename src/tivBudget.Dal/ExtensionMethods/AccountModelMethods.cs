using System;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.ExtensionMethods
{
  public static class AccountModelMethods
  {
    public static Account ExtractDirtyDbModels(this AccountOverview accountOverview)
    {
      Account returnAccount = null;

      if (accountOverview.IsDirty || accountOverview.IsChildDirty)
      {
        returnAccount = accountOverview.ToDbModel();
        foreach (var accountCategoryOverview in accountOverview.AccountCategories)
        {
          if (accountCategoryOverview.IsDirty || accountCategoryOverview.IsChildDirty)
          {
            var accountCategory = accountCategoryOverview.ToDbModel();
            foreach (var accountActualOverview in accountCategoryOverview.AccountActuals)
            {
              if (accountActualOverview.IsDirty || accountActualOverview.IsDeleted)
              {
                accountCategory.AccountActuals.Add(accountActualOverview.ToDbModel());
              }
            }
            returnAccount.AccountCategories.Add(accountCategory);
          }
        }
      }

      return returnAccount;
    }

    public static Account ToDbModel(this AccountOverview ao)
    {
      return new Account
      {
        Id = ao.Id,
        AccountTemplateId = ao.AccountTemplateId,
        AccountTypeId = ao.AccountTypeId,
        Description = ao.Description,
        OwnerId = ao.OwnerId,
        AreAccountCategoriesOpen = ao.AreAccountCategoriesOpen,
        DisplayIndex = ao.DisplayIndex,
        CreatedBy = ao.CreatedBy,
        CreatedOn = ao.CreatedOn,
        ModifiedBy = ao.ModifiedBy,
        ModifiedOn = ao.ModifiedOn,
        Ts = ao.Ts,
        IsEnabled = ao.IsEnabled,
        IsDefaultOfType = ao.IsDefaultOfType,
        IsNew = ao.IsNew,
        IsDirty = ao.IsDirty,
        IsDeleted = ao.IsDeleted
      };
    }

    public static AccountCategory ToDbModel(this AccountCategoryOverview aco)
    {
      return new AccountCategory
      {
        Id = aco.Id,
        CategoryTemplateId = aco.CategoryTemplateId,
        Description = aco.Description,
        AreAccountActualsOpen = aco.AreAccountActualsOpen,
        AccountId = aco.AccountId,
        DisplayIndex = aco.DisplayIndex,
        IsDefault = aco.IsDefault,
        CreatedBy = aco.CreatedBy,
        CreatedOn = aco.CreatedOn,
        ModifiedBy = aco.ModifiedBy,
        ModifiedOn = aco.ModifiedOn,
        Ts = aco.Ts,
        IsNew = aco.IsNew,
        IsDirty = aco.IsDirty,
        IsDeleted = aco.IsDeleted
      };
    }

    public static AccountActual ToDbModel(this AccountActualOverview aao)
    {
      return new AccountActual
      {
        Id = aao.Id,
        ActualTemplateId = aao.ActualTemplateId,
        BudgetActualLinkId = aao.BudgetActualLinkId,
        CategoryId = aao.CategoryId,
        Description = aao.Description,
        RelevantOn = aao.RelevantOn,
        Amount = aao.Amount,
        IsLinked = aao.IsLinked,
        IsRecurring = aao.IsRecurring,
        CreatedBy = aao.CreatedBy,
        CreatedOn = aao.CreatedOn,
        ModifiedBy = aao.ModifiedBy,
        ModifiedOn = aao.ModifiedOn,
        Ts = aao.Ts,
        IsBudgetDefaultLink = aao.IsBudgetDefaultLink,
        IsNew = aao.IsNew,
        IsDirty = aao.IsDirty,
        IsDeleted = aao.IsDeleted
      };
    }
  }
}