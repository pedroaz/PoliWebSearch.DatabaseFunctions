using PoliWebSearch.DatabaseFunctions.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PoliWebSearch.DatabaseFunctions.Data.DTO
{
    public class PersonDTO
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public List<string> Names { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public PersonDTO()
        {

        }

        /// <summary>
        /// Creates a PersonDTO from a Database result
        /// </summary>
        /// <param name="model"></param>
        public PersonDTO(DatabaseResultModel model)
        {
            Id = model?.id;
            Cpf = model?.properties["Cpf"]?.First()?.value;
            Names = new List<string>();
            foreach (var item in model?.properties["Names"]) {
                if (item.value != "") {
                    Names.Add(item.value);
                }
            }
            Names = Names.Distinct().ToList();
        }
    }
}
