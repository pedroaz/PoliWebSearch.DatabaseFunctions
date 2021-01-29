using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoliWebSearch.DatabaseFunctions.Services.Database
{
    public interface IDatabaseService
    {
        void Initialize();
        Task ExecuteQuery(string query);
    }
}
