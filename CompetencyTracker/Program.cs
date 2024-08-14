using CompetencyTracker.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapControllers();

app.Run();