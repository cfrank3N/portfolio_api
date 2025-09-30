using Xunit;
using PortfolioApi.Interfaces;
using Microsoft.Extensions.Configuration;
using PortfolioApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PortfolioApi.Tests
{
    public class GitHubServiceUnitTests : IClassFixture<CustomWebApplicationFactory>
    {

        private readonly CustomWebApplicationFactory _factory;
        private readonly IGitHubService _service;

        public GitHubServiceUnitTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            var scope = _factory.Services.CreateScope();
            _service = scope.ServiceProvider.GetRequiredService<IGitHubService>();
        }

        [Fact]
        public async Task GetPinnedReposShouldNotBeEmptyTest()
        {
            var result = await _service.GetPinnedRepos();

            Assert.NotNull(result.Data);
            Assert.True(result.Data.Count > 0);
        }

    }
}
