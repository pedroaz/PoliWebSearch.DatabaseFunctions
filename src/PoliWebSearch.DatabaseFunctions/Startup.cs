﻿using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PoliWebSearch.DatabaseFunctions.Factories.Database;
using PoliWebSearch.DatabaseFunctions.Services.Database;
using System.Reflection;

[assembly: FunctionsStartup(typeof(PoliWebSearch.DatabaseFunctions.Startup))]

namespace PoliWebSearch.DatabaseFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDatabaseQueryFactory, DatabaseQueryFactory>();
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
        }
    }
}
