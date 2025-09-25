using PortfolioApi.Entities;
using PortfolioApi.Utility;

namespace PortfolioApi.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(MessageProcessingHandler message);
    }
}
