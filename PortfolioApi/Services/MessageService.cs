
using PortfolioApi.DbContexts;
using PortfolioApi.Entities;
using PortfolioApi.Interfaces;
using PortfolioApi.Utility;

namespace PortfolioApi.Services
{
    public class MessageService :IMessageService
    {
        private readonly MessageContext _context;
        public MessageService(MessageContext context) 
        { 
            _context = context;
        }

        public async Task<Result<Message>> SaveMessage(Message message)
        {
            if (message == null)
            {
                return Result<Message>.Failure("Message can't be null");
            }
            try
            {
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return Result<Message>.Success(message, "Message created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Result<Message>.Failure("Error. All fields must be filled out");
            }
                
                
        }
    }
}
