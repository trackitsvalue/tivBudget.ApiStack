using freebyTech.Common.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories
{
    public class BudgetRepository : GenericRepository<Budget>
    {
        public BudgetRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Budget FindByIndex(string description, int month, int year)
        {
            throw new NotImplementedException();
        }
    }
}
