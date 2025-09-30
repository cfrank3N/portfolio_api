

using Microsoft.Extensions.DependencyInjection;
using PortfolioApi.DbContexts;
using PortfolioApi.Entities;
using PortfolioApi.Interfaces;

namespace PortfolioApi.Tests
{
    public class MessageServiceUnitTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly IMessageService _service;
        private readonly MessageContext _context;

        public MessageServiceUnitTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            var scope = _factory.Services.CreateScope();
            _service = scope.ServiceProvider.GetRequiredService<IMessageService>();
            _context = scope.ServiceProvider.GetRequiredService<MessageContext>();
        }

        [Fact]
        public async Task MessageServiceShoulAddMessageToDb()
        {

            // Arrange
            var message = new Message
            {
                SenderName = "Carro",
                SenderEmail = "carrohlund@hotmail.com",
                Content = "Svara då!"
            };

            // Act
            var savedMessage = await _service.SaveMessage(message);
            
            // Assert
            Assert.NotNull(savedMessage.Data);
            var messageFromDb = await _context.FindAsync<Message>(savedMessage.Data.Id);
            Assert.NotNull(messageFromDb);
            Assert.Equal(message.SenderEmail, messageFromDb.SenderEmail);
            Assert.Equal(message.Content, messageFromDb.Content);
        }


    }
}
