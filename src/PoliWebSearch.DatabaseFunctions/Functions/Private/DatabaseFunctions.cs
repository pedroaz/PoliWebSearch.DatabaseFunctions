using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using PoliWebSearch.DatabaseFunctions.DTO;
using PoliWebSearch.DatabaseFunctions.Services.Database;
using System.IO;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Functions.Private
{
    public class DatabaseFunctions
    {
        private IDatabaseService databaseService;

        public DatabaseFunctions(IDatabaseService databaseSerivce)
        {
            databaseService = databaseSerivce;
        }

        /// <summary>
        /// Executes a custom query into the database and return the response
        /// Grabs the Query form the post body
        /// </summary>
        /// <param name="request">Http request</param>
        /// <returns>The query result</returns>
        [FunctionName("ExecuteCustomQuery")]
        public async Task<IActionResult> ExecuteCustomQueryFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            var databaseQuery = await GetDatabaseQueryFromHttpRequest(request);
            var response = await databaseService.ExecuteQuery(databaseQuery.Query);
            return new OkObjectResult(response);
        }

        /// <summary>
        /// Gets the DatabaseQueryDTO from the request body
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<DatabaseQueryDTO> GetDatabaseQueryFromHttpRequest(HttpRequest req)
        {
            var jsonString = await new StreamReader(req.Body).ReadToEndAsync();
            var databaseQuery = JsonConvert.DeserializeObject<DatabaseQueryDTO>(jsonString);
            return databaseQuery;
        }

    }
}
