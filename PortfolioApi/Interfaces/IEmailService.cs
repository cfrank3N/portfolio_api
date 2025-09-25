using PortfolioApi.Entities;

namespace PortfolioApi.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(Message message);
    }
}
