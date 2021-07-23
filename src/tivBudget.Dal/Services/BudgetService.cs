using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tivBudget.Dal.Constants;
using tivBudget.Dal.ExtensionMethods;
using tivBudget.Dal.Models;
using tivBudget.Dal.VirtualModels;

namespace tivBudget.Dal.Services
{
  public static class BudgetService
  {
    public static Guid DebtCategory = new Guid("dcfc7a7b-5f86-e311-821b-00215e73190e");
    public static Guid SavingsCategory = new Guid("d8fc7a7b-5f86-e311-821b-00215e73190e");
    public static Guid CharityCategory = new Guid("9dc4caf1-0898-e311-821b-00215e73190e");

    public static Budget BuildNewBudget(string description, int year, int month, Guid userId, string userName)
    {
      return new Budget
      {
        CreatedBy = userName,
        CreatedOn = DateTime.Now,
        Description = description,
        Id = Guid.NewGuid(),
        OwnerId = userId,
        IsNew = true,
        IsDirty = true,
        StartDate = new DateTime(year, month, 1),
        Year = year,
        Month = month,
        BudgetCategories = new List<BudgetCategory>(),
      };
    }

    public static void UpgradeBudgetIfNeeded(this Budget budget, List<Account> accounts)
    {
      // Budget level default account linkage is a new feature.
      if (!budget.AccountLinkId.HasValue || !budget.AccountCategoryLinkId.HasValue)
      {
        Account selectedtAccount = null;

        // Try to give an intelligent default with graceful failure for defaulting account links, this is not required but much of the new
        // applications functionality is based upon the new default account linking.
        if (accounts.Count > 0)
        {
          if (!budget.AccountLinkId.HasValue)
          {
            selectedtAccount =
              AccountService.GetDefaultOrFirstOfAccountTypes(accounts, $"|{AccountTypeEnum.BankAccount:D}|");
            if (selectedtAccount == null)
            {
              selectedtAccount =
                AccountService.GetDefaultOrFirstOfAccountTypes(accounts, $"|{AccountTypeEnum.CashAccount:D}");
            }

            if (selectedtAccount != null)
            {
              budget.AccountLinkId = selectedtAccount.Id;
            }
          }
          else
          {
            selectedtAccount = AccountService.GetAccountFromId(accounts, budget.AccountLinkId.Value);
          }
        }

        if (selectedtAccount != null)
        {
          var selectedCategory =
            AccountService.GetDefaultOrFirstOfAccountCategories(selectedtAccount.AccountCategories.ToList());
          if (selectedCategory != null)
          {
            budget.AccountCategoryLinkId = selectedCategory.Id;
          }
        }
      }
    }

    //Accomplishment Tracking Methods
    //-------------------------------

    /// Verifies that the proper accomplishments exist on a budget
    public static bool VerifyAccomplishmentsOnBudget(User user, Budget budget, string modifyUser, DateTime modifyTime)
    {
      var userDbSaveRequired = false;
      var beginningOfMonth = new DateTime(budget.Year, budget.Month, 1);
      var endOfMonth = new DateTime(budget.Year, budget.Month, DateTime.DaysInMonth(budget.Year, budget.Month), 23, 59, 59);
      SystemAccomplishmentModel newExperienceType = null;
      if (budget.IsNew)
      {
        newExperienceType = AccomplishmentService.ExperienceTypes[(int)ExperienceTypeIds.FirstBudgetCreated];
        if (!user.HasAccomplishment(newExperienceType.Type, newExperienceType.SubType))
        {
          user.AddAccomplishment(newExperienceType, budget.Id, newExperienceType.Experience, modifyUser, modifyTime, string.Format(newExperienceType.Description, budget.Month, budget.Year));
          userDbSaveRequired = true;
        }
        else
        {
          // Earlybird accomplishment
          if (beginningOfMonth.Ticks > modifyTime.Ticks)
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCreatedEarlybird, modifyUser, modifyTime, ref userDbSaveRequired);

          }
          else if (beginningOfMonth.AddDays(5).Ticks > modifyTime.Ticks)
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCreated, modifyUser, modifyTime, ref userDbSaveRequired);
          }
        }
      }

      if (budget.ActualIncome > 0 && budget.ActualRemaining == 0)
      {
        // Are we between two days before end of month and 15 days after end of month?
        if (endOfMonth.AddDays(-3).Ticks < modifyTime.Ticks && modifyTime.Ticks < endOfMonth.AddDays(15).Ticks)
        {
          AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompleted, modifyUser, modifyTime, ref userDbSaveRequired);
          if (budget.RevolvingCreditSpending > 0 && budget.RevolvingCreditToPayOff <= 0)
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompletedCreditPaidOff, modifyUser, modifyTime, ref userDbSaveRequired);
          }
          var savingsCategory = budget.BudgetCategories.FirstOrDefault((bc) => bc.CategoryTemplateId.CompareTo(SavingsCategory) == 0);
          if (savingsCategory.CategorySpent >= 100)
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompletedSavedMoney, modifyUser, modifyTime, ref userDbSaveRequired);
          }
          var debtCategory = budget.BudgetCategories.FirstOrDefault((bc) => bc.CategoryTemplateId.CompareTo(DebtCategory) == 0);
          if (debtCategory.CategorySpent >= 100)
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompletedDebtPaidDown, modifyUser, modifyTime, ref userDbSaveRequired);
          }
          var charityCategory = budget.BudgetCategories.FirstOrDefault((bc) => bc.CategoryTemplateId.CompareTo(CharityCategory) == 0);
          if (charityCategory.CategorySpent >= Decimal.Multiply(budget.ActualIncome, 0.1M))
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompletedTithing, modifyUser, modifyTime, ref userDbSaveRequired);
          }
          else if (charityCategory.CategorySpent >= Decimal.Multiply(budget.ActualIncome, 0.05M))
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompletedHalfTithing, modifyUser, modifyTime, ref userDbSaveRequired);
          }
          else if (charityCategory.CategorySpent >= 100)
          {
            AddExperienceIfMissing(user, budget, (int)ExperienceTypeIds.BudgetCompletedCharitable, modifyUser, modifyTime, ref userDbSaveRequired);
          }
        }
      }
      else
      {
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompleted, ref userDbSaveRequired);
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedCreditPaidOff, ref userDbSaveRequired);
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedSavedMoney, ref userDbSaveRequired);
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedDebtPaidDown, ref userDbSaveRequired);
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedTithing, ref userDbSaveRequired);
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedHalfTithing, ref userDbSaveRequired);
        DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedCharitable, ref userDbSaveRequired);
      }

      return userDbSaveRequired;
    }

    public static bool DeleteAccomplishmentsIfExist(User user, Budget budget)
    {
      var userDbSaveRequired = false;

      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.FirstBudgetCreated, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCreatedEarlybird, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCreated, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompleted, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedCreditPaidOff, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedSavedMoney, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedDebtPaidDown, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedTithing, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedHalfTithing, ref userDbSaveRequired);
      DeleteExperienceIfFound(user, budget, (int)ExperienceTypeIds.BudgetCompletedCharitable, ref userDbSaveRequired);

      return userDbSaveRequired;
    }

    private static void AddExperienceIfMissing(User user, Budget budget, int experienceId, string modifyUser, DateTime modifyTime, ref bool userDbSaveRequired)
    {
      var newExperienceType = AccomplishmentService.ExperienceTypes[experienceId];
      if (!user.HasAccomplishmentWithAssociatedId(newExperienceType.Type, newExperienceType.SubType, budget.Id))
      {
        user.AddAccomplishment(newExperienceType, budget.Id, newExperienceType.Experience, modifyUser, modifyTime, string.Format(newExperienceType.Description, budget.Month, budget.Year));
        userDbSaveRequired = true;
      }
    }

    private static void DeleteExperienceIfFound(User user, Budget budget, int experienceId, ref bool userDbSaveRequired)
    {
      var newExperienceType = AccomplishmentService.ExperienceTypes[experienceId];
      if (user.DeleteAccomplishmentWithAssociatedIdIfExists(newExperienceType.Type, newExperienceType.SubType, budget.Id))
      {
        userDbSaveRequired = true;
      }
    }
  }
}
