using PortfolioApi.Models;

namespace PortfolioApi.Interfaces
{
    public interface IGitHubService
    {
        Task<List<GitHubRepo>> GetPinnedRepos();
    }
}
