using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Interfaces;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers
{
    [Route("api/repos")]
    [ApiController]
    public class GitHubController : ControllerBase
    {

        private readonly IGitHubService _gitHubService;
        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("pinned", Name = "PinnedRepos")]
        public async Task<ActionResult<List<GitHubRepo>>> GetPinnedRepos()
        {
            var result = await _gitHubService.GetPinnedRepos();

            if (result.Successful)
            {
                return Ok(result.Data);
            }
            
            return StatusCode(500, result.Message);
        }
    }
}
