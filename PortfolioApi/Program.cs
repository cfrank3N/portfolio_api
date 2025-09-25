using Microsoft.EntityFrameworkCore;
using PortfolioApi.DbContexts;
using PortfolioApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection
builder.Services.AddDbContextPool<MessageContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Scope services
builder.Services.AddScoped<MessageService>();

// Use controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

