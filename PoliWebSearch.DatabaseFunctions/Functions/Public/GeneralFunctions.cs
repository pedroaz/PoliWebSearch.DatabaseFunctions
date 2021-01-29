using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions
{
    public class GeneralFunctions
    {
        [FunctionName("Ping")]
        public static async Task<IActionResult> PingFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            return new OkObjectResult("true");
        }
    }
}
