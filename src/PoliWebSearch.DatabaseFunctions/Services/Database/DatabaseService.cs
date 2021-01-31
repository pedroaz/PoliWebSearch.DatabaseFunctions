using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        private DatabaseServiceStatus serviceStatus = DatabaseServiceStatus.NotInitialized;
        private static GremlinServer server;
        private static readonly string databaseNameVariable = "DatabaseName";
        private static readonly string graphNameVariable = "GraphName";
        private static readonly string hostNameVariable = "HostName";
        private static readonly string masterKeyVariable = "MasterKey";
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() {
            Formatting = Formatting.Indented
        };
        private readonly ILogger logger;

        public DatabaseService(ILogger logger)
        {
            this.logger = logger;
            Initialize();
        }

        // <inheritdoc/>
        private void Initialize()
        {
            try {
                var databaseName = Environment.GetEnvironmentVariable(databaseNameVariable);
                var graphName = Environment.GetEnvironmentVariable(graphNameVariable);
                var hostName = Environment.GetEnvironmentVariable(hostNameVariable);
                var masterKey = Environment.GetEnvironmentVariable(masterKeyVariable);
                var userName = $"/dbs/{databaseName}/colls/{graphName}";
                server = new GremlinServer(hostName, 443, true, userName, masterKey);
                serviceStatus = DatabaseServiceStatus.Started;
            }
            catch (Exception e) {
                logger.LogInformation(e.Message);
                serviceStatus = DatabaseServiceStatus.Faulted;
                throw;
            }
        }

        // <inheritdoc/>
        public async Task<string> ExecuteQuery(string query)
        {
            using var client = new GremlinClient(server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType);
            var resultSet = await client.SubmitAsync<dynamic>(query);
            JArray array = new JArray();
            foreach (var result in resultSet) {

                string jsonObject = JsonConvert.SerializeObject(result, jsonSerializerSettings);
                array.Add(JsonConvert.DeserializeObject(jsonObject));
            }
            return array.ToString();
        }

        // <inheritdoc/>
        public DatabaseServiceStatus GetDatabaseServiceStatus()
        {
            return serviceStatus;
        }
    }
}
