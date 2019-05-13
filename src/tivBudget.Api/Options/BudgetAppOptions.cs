using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tivBudget.Api.Options
{
    /// <summary>
    /// Class representing the Budget App Options environment variables used in the API part of the application.
    /// </summary>
    public class BudgetAppOptions
    {
      /// <summary>
      /// The Base URL for this API.
      /// </summary>
      public string ClientRootUrl { get; set; }

      /// <summary>
      /// The Authority for the Security Token Service.
      /// </summary>
      public string StsAuthority { get; set; }

      /// <summary>
      /// The ID that represents the client for the security token service.
      /// </summary>
      public string StsClientId { get; set; }

      /// <summary>
      /// The ID that represents the audience for this API.
      /// </summary>
      public string StsClientAudience { get; set; }
  }
}
