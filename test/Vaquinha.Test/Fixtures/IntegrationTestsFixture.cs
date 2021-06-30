using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using Vaquinha.App;
using Vaquinha.App.Config;
using Vaquinha.Test.Config;
using Xunit;

namespace Vaquinha.Test.Fixtures
{
    [CollectionDefinition(nameof(IntegrationWebTestsFixtureCollection))]
    public class IntegrationWebTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupWebTests>>
    {
    }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient Client;
        public IConfigurationRoot Configuration;
        public GlobalAppConfig ConfiguracaoGeralAplicacao;
        public readonly CrowfundingAppFactory<TStartup> Factory;

        public IntegrationTestsFixture()
        {
            var clientOption = new WebApplicationFactoryClientOptions
            {
            };

            Factory = new CrowfundingAppFactory<TStartup>();
            Client = Factory.CreateClient(clientOption);
            Configuration = GetConfiguration();

            ConfiguracaoGeralAplicacao = BuildGlobalAppConfiguration();
        }

        private GlobalAppConfig BuildGlobalAppConfiguration()
        {   
            var globalAppSettings = new GlobalAppConfig();
            Configuration.Bind("ConfiguracoesGeralAplicacao", globalAppSettings);

            return globalAppSettings;
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        private IConfigurationRoot GetConfiguration()
        {
            var workingDir = Directory.GetCurrentDirectory();

            return new ConfigurationBuilder()
                      .SetBasePath(workingDir)
                      .AddJsonFile("appsettings.json")
                      .AddJsonFile("appsettings.Testing.json")
                      .Build();
        }
    }
}