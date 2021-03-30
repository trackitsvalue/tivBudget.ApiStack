using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IBudgetRepository
  {
    Budget FindByIndex(Guid ownerIdOrContributorId, string description, int month, int year);
    Budget FindById(Guid ownerIdOrContributorId, Guid budgetId);
    void Upsert(Budget budget, string userName);
    List<Budget> FindCountByOwner(Guid ownerId, int count);
    List<Budget> FindAllByOwner(Guid ownerId);
    int FindCountByOwner(Guid ownerIdOrContributorId);

    void Delete(Guid ownerIdOrContributorId, Guid budgetId);
  }
}
