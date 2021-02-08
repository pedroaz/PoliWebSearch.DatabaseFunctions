using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using PoliWebSearch.DatabaseFunctions.Data.DTO;
using PoliWebSearch.DatabaseFunctions.Data.Models;
using PoliWebSearch.DatabaseFunctions.Factories.Database;
using PoliWebSearch.DatabaseFunctions.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try {
                var query = databaseQueryFactory.CreateGetPersonInformationByCpfQuery(cpf);
                var queryResults = await ExecuteDatabaseOperation(query);

                if (queryResults.Any()) {
                    return new OkObjectResult(new PersonDTO(queryResults.First()));
                }
                else {
                    return new OkObjectResult(new PersonDTO());
                }
            }
            catch (Exception e) {

                return new BadRequestObjectResult($"Uncaught error:\n {e.Message}");
            }
        }

        [FunctionName("SearchPersonByName")]
        public async Task<IActionResult> SearchPersonByNameFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SearchPersonByName/{name}")] HttpRequest request,
            string name)
        {
            try {
                var query = databaseQueryFactory.CreateSearchPersonByNameQuery(name, GetMaxAmountOfResuts(request));
                var queryResults = await ExecuteDatabaseOperation(query);

                if (queryResults.Any()) {
                    var data = new SearchPersonByNameDTO(queryResults);
                    return new OkObjectResult(data);
                }
                else {
                    return new OkObjectResult(new SearchPersonByNameDTO());
                }
            }
            catch (Exception e) {

                return new BadRequestObjectResult($"Uncaught error:\n {e.Message}");
            }
        }

        private static int GetMaxAmountOfResuts(HttpRequest request)
        {
            var urlParameter = request.Query["maxAmountOfResults"];
            if (urlParameter.Any()) {
                if (int.TryParse(urlParameter.First(), out int result)) {
                    return result;
                }
            }
            return 49;
        }

        private async Task<List<DatabaseResultModel>> ExecuteDatabaseOperation(string query)
        {
            var databaseJsonResponse = await databaseService.ExecuteQuery(query);
            return JsonConvert.DeserializeObject<List<DatabaseResultModel>>(databaseJsonResponse);
        }
    }
}
