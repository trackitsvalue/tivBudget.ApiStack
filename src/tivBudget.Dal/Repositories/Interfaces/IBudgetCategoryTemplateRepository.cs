using System;
using System.Collections.Generic;
using tivBudget.Dal.Models;

namespace tivBudget.Dal.Repositories.Interfaces
{
  public interface IBudgetCategoryTemplateRepository
  {
    List<BudgetCategoryTemplate> FindAllTemplatesByOwner(Guid ownerId);
  }
}
