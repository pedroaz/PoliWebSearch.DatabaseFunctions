using PoliWebSearch.DatabaseFunctions.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoliWebSearch.DatabaseFunctions.DTO
{
    public class PersonDataDTO
    {
        public string Id { get; set; }
        public string Cpf { get; set; }
        public List<string> Names { get; set; }

        public PersonDataDTO(DatabaseResultModel model)
        {
            Id = model?.id;
            Cpf = model?.properties["Cpf"]?.First()?.value;
            Names = new List<string>();
            foreach (var item in model?.properties["Names"]) {
                if(item.value != "") {
                    Names.Add(item.value);
                }
            }
            Names = Names.Distinct().ToList();
        }
    }
}
