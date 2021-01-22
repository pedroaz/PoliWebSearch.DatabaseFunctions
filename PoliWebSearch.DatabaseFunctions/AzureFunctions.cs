using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PoliWebSearch.DatabaseFunctions
{
    public static class AzureFunctions
    {

        [FunctionName("Ping")]
        public static async Task<IActionResult> PingFunction(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Service was pinged");
            return new OkObjectResult("true");
        }

        [FunctionName("ExecuteCustomQuery")]
        public static async Task<IActionResult> ExecuteCustomQueryFunction(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return new OkObjectResult("Query Executed Successfully");
        }
    }
}
