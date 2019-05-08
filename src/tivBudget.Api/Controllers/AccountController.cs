using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Account API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private IAccountRepository AccountRepo { get; }

    /// <summary>
    /// Standard constructor.
    /// </summary>
    /// <param name="accountRepository">Repo to use for account information.</param>
    public AccountController(IAccountRepository accountRepository)
    {
      AccountRepo = accountRepository;
    }

    /// <summary>
    /// Returns all accounts owned by a given user.
    /// </summary>
    [HttpGet("all")]
    public IActionResult Get()
    {
      // Me
      // var userId = new Guid("3DC480F1-5586-E311-821B-00215E73190E");
      // Demo User
      var userId = new Guid("A74E2E16-8338-E411-B92D-00215E73190E");
      var accounts = AccountRepo.FindAllByOwner(userId);

      return Ok(CleanDoubleReferences(accounts));
    }

    private List<Account> CleanDoubleReferences(List<Account> accounts)
    {
      if (accounts != null && accounts.Count > 0)
      {
        foreach (var account in accounts)
        {
          account.AccountTemplate.Accounts = null;
        }
      }

      return accounts;
    }
  }
}