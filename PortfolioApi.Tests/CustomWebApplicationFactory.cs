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
    public class CustomWebApplicationFactory<TProgram> : 
        WebApplicationFactory<TProgram> where TProgram : class, IAsyncLifetime
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
                var dbDescriptor = services.SingleOrDefault(d => 
                d.ServiceType == typeof(IDbContextOptionsConfiguration<MessageContext>));

                if (dbDescriptor != null)
                    services.Remove(dbDescriptor);

                services.AddDbContext<MessageContext>(opt =>
                    opt.UseNpgsql(_dbContainer.GetConnectionString()));
               
            });
        }

        public Task InitializeAsync()
        {
            return _dbContainer.StartAsync();
        }

        public new Task DisposeAsync()
        {
            return _dbContainer.StopAsync();
        }

    }
}
