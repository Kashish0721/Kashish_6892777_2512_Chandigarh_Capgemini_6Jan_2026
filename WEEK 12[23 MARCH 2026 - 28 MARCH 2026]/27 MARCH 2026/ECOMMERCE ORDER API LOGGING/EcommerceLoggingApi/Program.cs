using log4net;
using log4net.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Configure Log4net
var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());

var app = builder.Build();

// 🔹 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Middleware
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // important for controllers

// 🔹 Sample Minimal API (with logging)
var log = LogManager.GetLogger(typeof(Program));

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild",
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    log.Info("Weather API called");

    try
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();

        log.Info("Weather data generated successfully");

        return Results.Ok(forecast);  // 🔥 IMPORTANT FIX
    }
    catch (Exception ex)
    {
        log.Error("Weather API failed", ex);
        return Results.Problem("Error occurred");
    }
});
app.Run();


// 🔹 Model
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}