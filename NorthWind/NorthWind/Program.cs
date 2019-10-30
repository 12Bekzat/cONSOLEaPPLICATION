using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace HomeWork5
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();

            var connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
            var providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single(item => item.Key == "ProviderName").Value;

            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

            BetaConsoleApplication consoleApplication = new BetaConsoleApplication(connectionString, providerName);
            consoleApplication.Begin();
        }
    }
}
