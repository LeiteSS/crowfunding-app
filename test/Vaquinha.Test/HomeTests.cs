using FluentAssertions;
using System.Threading.Tasks;
using Vaquinha.App;
using Vaquinha.App.Extensions;
using Vaquinha.Test.Fixtures;
using Xunit;

namespace Vaquinha.Test
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class HomeTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _integrationTestsFixture;

        public HomeTests(IntegrationTestsFixture<StartupWebTests> integrationTestsFixture)
        {
            _integrationTestsFixture = integrationTestsFixture;
        }

        [Trait("HomeControllerIntegrationTests", "HomeController_CarregarPaginaInicial_TotalDoadoresETotalValorArrecadadoDeveSerZero")]
        [Fact]
        public async Task HomeController_CarregarPaginaInicial_TotalDoadoresETotalValorArrecadadoDeveSerZero()
        {
            // Arrange & Act
            var home = await _integrationTestsFixture.Client.GetAsync("Home");

            // Assert
            home.EnsureSuccessStatusCode();
            var dadosHome = await home.Content.ReadAsStringAsync();

            var collectedAmount = 0.ToBRLString();
            var goalCrowfunding = _integrationTestsFixture.ConfiguracaoGeralAplicacao.GoalCrowfunding.ToBRLString();

            // Dados totais da doação
            dadosHome.Should().Contain(expected: "Arrecadamos quanto?");
            dadosHome.Should().Contain(expected: collectedAmount);

            dadosHome.Should().Contain(expected: "Quanto falta arrecadar?");
            dadosHome.Should().Contain(expected: goalCrowfunding);
        }
    }
}