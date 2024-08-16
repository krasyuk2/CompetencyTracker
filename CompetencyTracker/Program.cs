using CompetencyTracker.Core.Abstractions;
using CompetencyTracker.DataAccess;
using CompetencyTracker.DataAccess.Repositories;
using CompetencyTracker.Middleware;
using CompetencyTracker.Services;
using CompetencyTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();


builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});


builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
     var service = scope.ServiceProvider.GetRequiredService<PersonDbContext>();
     service.Database.Migrate();
     
}

var useSwagger = builder.Configuration.GetValue<bool>("UseSwagger");
if (app.Environment.IsDevelopment() || useSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();