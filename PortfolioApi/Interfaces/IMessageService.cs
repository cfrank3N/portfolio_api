using PortfolioApi.Entities;

namespace PortfolioApi.Interfaces
{
    public interface IMessageService
    {
        Task<Result<Message>> SaveMessage(Message message);
    }
}
