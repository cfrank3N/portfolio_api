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
        private readonly IEmailService _emailService;

        public MessageController(IMessageService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
        }

        [HttpPost("savemessage")]
        public async Task<ActionResult<Message>> SaveMessageToDatabase([FromBody] Message message)
        {
            var result = await _service.SaveMessage(message);

            if (result.Successful)
            {
                _emailService.SendEmail(message);
                return CreatedAtAction(null, null, message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
