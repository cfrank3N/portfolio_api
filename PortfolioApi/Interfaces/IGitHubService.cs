using PortfolioApi.Models;
using PortfolioApi.Utility;

namespace PortfolioApi.Interfaces
{
    public interface IGitHubService
    {
        Task<Result<List<GitHubRepo>>> GetPinnedRepos();
    }
}
