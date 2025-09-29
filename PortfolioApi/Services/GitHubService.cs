using Octokit.GraphQL;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Model;
using PortfolioApi.Models;
using static Octokit.GraphQL.Variable;

namespace PortfolioApi.Services
{
    public class GitHubService
    {
        private readonly IConfiguration _configuration;
        private readonly Connection _connection;

        public GitHubService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new Connection(new ProductHeaderValue("PortfolioApi"), 
                _configuration["ApiKeys:GitHubToken"]);
        }

        public async Task<List<GitHubRepo>> GetPinnedRepos()
        {
            // Create a new query and instantiate
            // a list of objects of type Repository
            
            var query = new Query()
               .User("cfrank3N")
               .PinnedItems(first:6, types: new [] { PinnableItemType.Repository } )
               .Nodes
               .OfType<Repository>()
               .Select(repo => new GitHubRepo
               {
                   Name = repo.Name,
                   Description = repo.Description,
                   Url = repo.Url
               })
               .Compile();

            var result = await _connection.Run(query);

            return result.ToList();
        }
    }
}
