using Octokit.GraphQL;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Model;
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

        public async Task GetPinnedRepos()
        {
            // Create a new query and instantiate
            // a list of objects of type GitHubRepo
            
            var query = new Query()
               .User("cfrank3N")
               .PinnedItems(first:6, types: new[] { PinnableItemType.Repository } )
               .Nodes
               .OfType<Repository>()
               .Select(repo => new
               {
                   repo.Name,
                   repo.Description,
                   repo.Url
               })
               .Compile();

            var result = await _connection.Run(query);

            foreach (var repo in result)
            {
                Console.WriteLine(repo.Url);
                Console.WriteLine(repo.GetType);
            }

        }
    }
}
