var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Use(async (context, next) =>
{
    Console.WriteLine("➡️ Request: " + context.Request.Path);
    await next();
    Console.WriteLine("⬅️ Response sent");
});


app.MapGet("/", () => "Hello World!");

app.Run();
