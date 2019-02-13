using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
    public interface IBudgetRepository
    {
        Budget FindByIndex(Guid ownerIdOrContributorId, string description, int month, int year);
    }
}
