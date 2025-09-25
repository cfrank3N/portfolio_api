using Microsoft.AspNetCore.Mvc;
using PortfolioApi.CustomFilters;
using PortfolioApi.Entities;
using PortfolioApi.Interfaces;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers
{
    [Route("/api")]
    [ApiController]
    [MyCustomActionFilter]
    public class MessageController : ControllerBase
    {

        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpPost("savemessage")]
        public async Task<ActionResult<Message>> SaveMessageToDatabase([FromBody] Message message)
        {
            var result = await _service.SaveMessage(message);

            return result.Successful ? CreatedAtAction(null, null, message) : BadRequest(result.Message);
        }

    }
}
