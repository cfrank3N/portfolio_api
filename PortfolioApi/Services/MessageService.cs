using PortfolioApi.Controllers;
using PortfolioApi.DbContexts;

namespace PortfolioApi.Services
{
    public class MessageService
    {
        private readonly MessageContext _context;
        public MessageService(MessageContext context) 
        { 
            _context = context;
        }
    }
}
