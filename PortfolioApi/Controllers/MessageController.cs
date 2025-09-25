using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers
{
    [Route("/api")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly MessageService _service;

        public MessageController(MessageService service)
        {
            _service = service;
        }

    }
}
