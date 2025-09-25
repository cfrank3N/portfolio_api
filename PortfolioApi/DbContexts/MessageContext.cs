using Microsoft.EntityFrameworkCore;
using PortfolioApi.Entities;

namespace PortfolioApi.DbContexts
{
    public class MessageContext(DbContextOptions<MessageContext> options) : DbContext(options)
    {
        public DbSet<Message> Messages { get; set; }
    }
}
