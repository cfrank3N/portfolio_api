using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers
{
    [Route("api/repos")]
    [ApiController]
    public class GitHubController : ControllerBase
    {

        private readonly GitHubService _gitHubService;
        public GitHubController(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("pinned", Name = "PinnedRepos")]
        public async Task<List<GitHubRepo>> GetPinnedRepos()
        {
            return await _gitHubService.GetPinnedRepos();
        }
    }
}
