using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Entities;
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

        [HttpPost("savemessage")]
        public async Task<ActionResult<Message>> SaveMessageToDatabase([FromBody] Message message)
        {
            var result = await _service.SaveMessage(message);

            return result.Successful ? CreatedAtAction(null, null, message) : BadRequest(result.Message);
        }

    }
}
