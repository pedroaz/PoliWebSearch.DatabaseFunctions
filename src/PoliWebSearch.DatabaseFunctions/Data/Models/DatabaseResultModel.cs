using PoliWebSearch.DatabaseFunctions.DTO;
using System.Collections.Generic;

namespace PoliWebSearch.DatabaseFunctions.Data.Models
{
    public class DatabaseResultModel
    {
        public string id { get; set; }
        public Dictionary<string, List<PropertyPair>> properties { get; set; }
    }
}
