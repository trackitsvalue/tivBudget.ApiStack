using System;
using tivBudget.Dal.Models;

namespace tivBudget.Api.Services.Interfaces
{
  public interface IAccountService
  {
    AllAccountsOverview GetAllAccountsOverview(Guid ownerId, int year, int month);
  }
}
