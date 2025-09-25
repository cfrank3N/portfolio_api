using PortfolioApi.Entities;
using PortfolioApi.Utility;

namespace PortfolioApi.Interfaces
{
    public interface IMessageService
    {
        Task<Result<Message>> SaveMessage(Message message);
    }
}
