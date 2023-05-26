using System.Text.Json.Serialization;
using NightSky.API.Extensions;
using NightSky.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;;
});

builder.Logging.ClearProviders().AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterDbContext()
    .RegisterAutoMapper()
    .RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseCors(x =>
{
    // w prawdziwym świecie powinno być tu coś prawdziwego ustawionego
    x.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();