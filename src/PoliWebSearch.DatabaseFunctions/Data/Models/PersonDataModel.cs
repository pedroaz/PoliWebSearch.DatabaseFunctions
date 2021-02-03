using PoliWebSearch.DatabaseFunctions.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoliWebSearch.DatabaseFunctions.Data.Models
{
    public class PersonDataModel
    {
        public string id { get; set; }
        public Dictionary<string, List<PropertyPair>> properties { get; set; }
    }
}
