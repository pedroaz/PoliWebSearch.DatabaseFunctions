using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using PoliWebSearch.DatabaseFunctions.Factories.Database;
using PoliWebSearch.DatabaseFunctions.Services.Database;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Functions.Public.People
{
    public class PeopleFunctions
    {
        private readonly IDatabaseQueryFactory databaseQueryFactory;
        private readonly IDatabaseService databaseService;

        public PeopleFunctions(IDatabaseQueryFactory databaseQueryFactory, IDatabaseService databaseService)
        {
            this.databaseQueryFactory = databaseQueryFactory;
            this.databaseService = databaseService;
        }

        [FunctionName("GetPersonInformationByCpf")]
        public async Task<IActionResult> GetPersonInformationByCpfFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetPersonInformationByCpf/{cpf}")] HttpRequest request,
            string cpf)
        {
            var query = databaseQueryFactory.CreateGetPersonInformationByCpfQuery(cpf);
            var response = await databaseService.ExecuteQuery(query);
            return new OkObjectResult(response);
        }
    }
}
