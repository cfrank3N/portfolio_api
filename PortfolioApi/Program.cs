using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.DbContexts;
using PortfolioApi.Interfaces;
using PortfolioApi.Services;

string MyAllowSpecificOrigins = "_myAllowedOrigins";

var builder = WebApplication.CreateBuilder(args);

// Configure cors to allow calls from my frontend
builder.Services.AddCors(options =>
    options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {
                            policy.WithOrigins("https://portfolio-adam-frank.netlify.app",
                                               "http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        })
    );

// Configure database connection
builder.Services.AddDbContextPool<MessageContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Scope services
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IEmailService,  EmailService>();
builder.Services.AddScoped<IGitHubService, GitHubService>();

// Disables ASP.NET's automatic 400 response for validation failures on models/entities
// Lets me handle them by creating a custom Filter that inherits ActionFilterAttribute
builder.Services.Configure<ApiBehaviorOptions>(opt =>
    opt.SuppressModelStateInvalidFilter = true);

// Use controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();

// Expose Program.cs to the test project to be able to test the app
public partial class Program { };

