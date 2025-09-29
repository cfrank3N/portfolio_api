using Octokit.GraphQL;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Model;
using PortfolioApi.Interfaces;
using PortfolioApi.Models;
using PortfolioApi.Utility;
using static Octokit.GraphQL.Variable;

namespace PortfolioApi.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IConfiguration _configuration;
        private readonly Connection _connection;

        public GitHubService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new Connection(new ProductHeaderValue("PortfolioApi"), 
                _configuration["ApiKeys:GitHubToken"]);
        }

        public async Task<Result<List<GitHubRepo>>> GetPinnedRepos()
        {
            // Create a new query and create GitHubRepo objects to return as list
            
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

            try
            {
                var listOfRepos = result.ToList();
                return Result<List<GitHubRepo>>.Success(listOfRepos, "successfully fetched repos");
            }
            catch (ArgumentNullException e)
            {
                return Result<List<GitHubRepo>>.Failure("No repos available to fetch");
            }
        }
    }
}
