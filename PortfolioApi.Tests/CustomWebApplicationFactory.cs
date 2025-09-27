using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PortfolioApi.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

namespace PortfolioApi.Tests
{
    public class CustomWebApplicationFactory : 
        WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("testdb")
            .WithUsername("testboy")
            .WithPassword("testboy")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => 
                d.ServiceType == typeof(DbContextOptions<MessageContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContextPool<MessageContext>(opt =>
                    opt.UseNpgsql(_dbContainer.GetConnectionString()));
               
            });
        }

        public async Task InitializeAsync()
        {
             await _dbContainer.StartAsync();

            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MessageContext>();

            await context.Database.MigrateAsync();
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }

    }
}
