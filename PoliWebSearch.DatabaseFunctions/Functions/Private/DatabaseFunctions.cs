using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Functions.Private
{
    public class DatabaseFunctions
    {
        private static readonly GremlinServer server;
        private static readonly string databaseNameVariable = "DatabaseName";
        private static readonly string graphNameVariable = "GraphName";
        private static readonly string hostNameVariable = "HostName";
        private static readonly string masterKeyVariable = "MasterKey";

        static DatabaseFunctions()
        {
            var databaseName = Environment.GetEnvironmentVariable(databaseNameVariable);
            var graphName = Environment.GetEnvironmentVariable(graphNameVariable);
            var hostName = Environment.GetEnvironmentVariable(hostNameVariable);
            var masterKey = Environment.GetEnvironmentVariable(masterKeyVariable);
            var userName = $"/dbs/{databaseName}/colls/{graphName}";
            server = new GremlinServer(hostName, 443, true, userName, masterKey);
        }

        [FunctionName("ExecuteCustomQuery")]
        public static async Task<IActionResult> ExecuteCustomQueryFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {
            var databaseQuery = await GetDatabaseQuery(req);
            using var client = new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType);
            var queryResult = await client.SubmitAsync<dynamic>(databaseQuery.Query);

            StringBuilder sb = new StringBuilder();
            foreach (var item in queryResult) {
                sb.Append(JsonConvert.SerializeObject(item));
            }

            return new OkObjectResult(sb.ToString());
        }

        private static async Task<DatabaseQueryDTO> GetDatabaseQuery(HttpRequest req)
        {
            string query = req.Query["Query"];
            var jsonString = await new StreamReader(req.Body).ReadToEndAsync();
            var databaseQuery = JsonConvert.DeserializeObject<DatabaseQueryDTO>(jsonString);
            return databaseQuery;
        }
    }
}
