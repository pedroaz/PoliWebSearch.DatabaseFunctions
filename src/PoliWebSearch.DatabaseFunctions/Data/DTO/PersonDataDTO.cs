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
        public string CandidateName { get; set; }

        public PersonDataDTO(PersonDataModel model)
        {
            Id = model?.id;
            Cpf = model?.properties["Cpf"]?.First()?.value;
            CandidateName = model?.properties["CandidateName"]?.First()?.value;
        }
    }
}
