using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Functions.Public
{
    public class GeneralFunctions
    {
        /// <summary>
        /// Pings the azure function and always returns true
        /// </summary>
        /// <param name="req">The http request</param>
        /// <returns></returns>
        [FunctionName("Ping")]
        public async Task<IActionResult> PingFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            return new OkObjectResult("true");
        }
    }
}
