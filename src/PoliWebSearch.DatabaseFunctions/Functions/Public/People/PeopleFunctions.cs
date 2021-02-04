using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using PoliWebSearch.DatabaseFunctions.Data.Models;
using PoliWebSearch.DatabaseFunctions.DTO;
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

                var databaseJsonResponse = await databaseService.ExecuteQuery(query);

                var models = JsonConvert.DeserializeObject<List<DatabaseResultModel>>(databaseJsonResponse);

                if (models.Any()) {
                    var data = new PersonDataDTO(models.First());
                    return new OkObjectResult(data);
                }
                else {
                    return new OkObjectResult("No person found with this CPF");
                }


            }
            catch (Exception e) {

                return new BadRequestObjectResult($"Uncaught error:\n {e.Message}");
            }
            
        }
    }
}
