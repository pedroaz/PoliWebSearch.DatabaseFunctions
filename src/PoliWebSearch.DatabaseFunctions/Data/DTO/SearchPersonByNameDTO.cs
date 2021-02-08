using PoliWebSearch.DatabaseFunctions.Data.Models;
using System.Collections.Generic;

namespace PoliWebSearch.DatabaseFunctions.Data.DTO
{
    public class SearchPersonByNameDTO
    {
        public List<PersonDTO> People { get; set; }

        public SearchPersonByNameDTO()
        {
            People = new List<PersonDTO>();
        }

        public SearchPersonByNameDTO(List<DatabaseResultModel> models)
        {
            People = new List<PersonDTO>();
            foreach (var model in models) {
                People.Add(new PersonDTO(model));
            }
        }
    }
}
