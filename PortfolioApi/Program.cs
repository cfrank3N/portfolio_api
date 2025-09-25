using Microsoft.EntityFrameworkCore;
using PortfolioApi.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection
builder.Services.AddDbContextPool<MessageContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Use controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

