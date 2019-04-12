using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tivBudget.Api.Models;
using tivBudget.Dal.Models;
using tivBudget.Dal.Repositories.Interfaces;

namespace tivBudget.Api.Controllers
{
  /// <summary>
  /// Budget API Controller.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class UtilityController : ControllerBase
  {
    /// <summary>
    /// Standard constructor.
    /// </summary>
    public UtilityController()
    {
    }
    /// <summary>
    /// Returns a list of GUIDs that can be used for new element construction in the application on the client side.
    /// </summary>
    /// <param name="count">The number of IDs to return.</param>
    /// <returns>A fully populated budget object.</returns>
    [HttpGet("ids/{count}")]
    public IActionResult Get(int count)
    {
      List<Guid> ids = new List<Guid>();

      for (var inc = 0; inc < count; inc++)
      {
        ids.Add(Guid.NewGuid());
      }
      return Ok(ids);
    }
  }
}