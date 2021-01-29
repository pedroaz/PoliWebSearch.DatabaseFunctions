using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PoliWebSearch.DatabaseFunctions.Services.Database;

[assembly: FunctionsStartup(typeof(PoliWebSearch.DatabaseFunctions.Startup))]

namespace PoliWebSearch.DatabaseFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
        }
    }
}
