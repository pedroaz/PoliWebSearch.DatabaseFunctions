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
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PoliWebSearch.DatabaseFunctions.Functions.Private
{
    public class DatabaseFunctions
    {
        private static readonly GremlinServer server;
        private static readonly string databaseNameVariable = "DatabaseName";
        private static readonly string graphNameVariable = "GraphName";
        private static readonly string hostNameVariable = "HostName";
        private static readonly string masterKeyVariable = "MasterKey";
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() {
            Formatting = Formatting.Indented
        };

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

            var resultSet = await client.SubmitAsync<dynamic>(databaseQuery.Query);
            JArray array = new JArray();
            foreach (var result in resultSet) {

                // I have no idea why we need to do this shit, but it work.
                string jsonObject = JsonConvert.SerializeObject(result, jsonSerializerSettings);
                array.Add(JsonConvert.DeserializeObject(jsonObject));
            }

            return new OkObjectResult(array.ToString());
        }

        private static async Task<DatabaseQueryDTO> GetDatabaseQuery(HttpRequest req)
        {
            var jsonString = await new StreamReader(req.Body).ReadToEndAsync();
            var databaseQuery = JsonConvert.DeserializeObject<DatabaseQueryDTO>(jsonString, jsonSerializerSettings);
            return databaseQuery;
        }
    }
}
