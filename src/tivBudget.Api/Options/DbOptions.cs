using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tivBudget.Api.Options
{
    /// <summary>
    /// Class representing the DB Options environment variables to setup applications DB connection.
    /// </summary>
    public class DbOptions
    {
        /// <summary>
        /// The Server Name of the SQL Server to connect to for DB operations, pulled from the environment variable DB__ServerName most likely.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// The User Name of the SQL Server to connect to for DB operations, pulled from the environment variable DB__UserName most likely.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The User Password of the SQL Server to connect to for DB operations, pulled from the environment variable DB__UserPassword most likely.
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Builds a standard connection string from the environment variables pulled from configuration.
        /// </summary>
        /// <returns></returns>
        public static string BuildConnectionString(string servername, string userName, string userPassword, string initialDb)
        {
            return ($"Data Source={servername};User ID={userName};Password={userPassword};Database={initialDb};Pooling=False;");
        }
    }
}
