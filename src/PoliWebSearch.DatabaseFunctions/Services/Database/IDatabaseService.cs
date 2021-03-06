﻿using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Services.Database
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Executes a custom query on the database and returns as a json
        /// </summary>
        /// <param name="query"></param>
        /// <returns>json result</returns>
        Task<string> ExecuteQuery(string query);

        /// <summary>
        /// Gets the database service
        /// </summary>
        /// <returns></returns>
        DatabaseServiceStatus GetDatabaseServiceStatus();
    }
}
