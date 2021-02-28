using freebyTech.Common.Data;
using System;
using System.Linq;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using freebyTech.Common.ExtensionMethods;

namespace tivBudget.Dal.Repositories
{
  public class AccountRepository : GenericRepository<Account>, IAccountRepository
  {
    public AccountRepository(freebyTrackContext dbContext) : base(dbContext)
    {
    }

    public List<Account> FindAllByOwner(Guid ownerId)
    {
      return QueryIncludingAllAccountEntitiesMinusActuals().Where(a => a.OwnerId == ownerId).ToList();
    }

    public List<AccountOverview> FindAllByOwnerAndMonth(Guid ownerId, int year, int month)
    {
      var startOfMonth = new DateTime(year, month, 1).StartOfDay();
      var endOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month)).EndOfDay();

      return Queryable().Where(a => a.OwnerId == ownerId)
        .Select(a => new AccountOverview
        {
          Id = a.Id,
          AccountTemplateId = a.AccountTemplateId,
          AccountTypeId = a.AccountTypeId,
          Description = a.Description,
          OwnerId = a.OwnerId,
          AreAccountCategoriesOpen = a.AreAccountCategoriesOpen,
          DisplayIndex = a.DisplayIndex,
          IsEnabled = a.IsEnabled,
          IsDefaultOfType = a.IsDefaultOfType,
          AccountTemplate = a.AccountTemplate,
          AccountType = a.AccountType,
          CreatedOn = a.CreatedOn,
          CreatedBy = a.CreatedBy,
          ModifiedOn = a.ModifiedOn,
          ModifiedBy = a.ModifiedBy,
          Ts = a.Ts,
          AccountCategories = a.AccountCategories.Select(ac => new AccountCategoryOverview
          {
            Id = ac.Id,
            CategoryTemplateId = ac.CategoryTemplateId,
            Description = ac.Description,
            AreAccountActualsOpen = ac.AreAccountActualsOpen,
            AccountId = ac.AccountId,
            DisplayIndex = ac.DisplayIndex,
            IsDefault = ac.IsDefault,
            CreatedOn = ac.CreatedOn,
            CreatedBy = ac.CreatedBy,
            ModifiedOn = ac.ModifiedOn,
            ModifiedBy = ac.ModifiedBy,
            Ts = ac.Ts,
            AccountActuals = ac.AccountActuals.Where(aa => aa.RelevantOn >= startOfMonth && aa.RelevantOn <= endOfMonth).Select(aa => new AccountActualOverview
            {
              Id = aa.Id,
              ActualTemplateId = aa.ActualTemplateId,
              BudgetActualLink = aa.BudgetActualLink,
              BudgetActualLinkId = aa.BudgetActualLinkId,
              CategoryId = aa.CategoryId,
              Description = aa.Description,
              RelevantOn = aa.RelevantOn,
              Amount = aa.Amount,
              IsLinked = aa.IsLinked,
              IsRecurring = aa.IsRecurring,
              CreatedOn = aa.CreatedOn,
              CreatedBy = aa.CreatedBy,
              ModifiedOn = aa.ModifiedOn,
              ModifiedBy = aa.ModifiedBy,
              Ts = aa.Ts,
            }).OrderByDescending(aa => aa.RelevantOn).ToList()
          }).OrderBy(ac => ac.DisplayIndex).ToList(),
        }).OrderBy(a => a.DisplayIndex).ToList();
    }

    /// <summary>
    /// Gets the count of accounts that this user is owner of or contributor to.
    /// </summary>
    /// <param name="ownerIdOrContributorId"></param>
    /// <returns>Account count.</returns>
    public int FindCountByOwner(Guid ownerIdOrContributorId)
    {
      return Queryable().Where(a => a.OwnerId == ownerIdOrContributorId).Count();
    }

    private IQueryable<Account> QueryIncludingAllAccountEntitiesMinusActuals()
    {
      return Queryable()
        .Include(a => a.AccountCategories)
        .Include(a => a.AccountTemplate);
    }
  }
}
