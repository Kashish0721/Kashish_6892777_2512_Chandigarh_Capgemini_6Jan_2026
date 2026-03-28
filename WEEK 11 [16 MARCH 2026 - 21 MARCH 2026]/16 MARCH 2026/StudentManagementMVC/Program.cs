using StudentManagementMVC.Filters;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CustomExceptionFilter>(); // global exception
});

builder.Services.AddSession();
builder.Services.AddScoped<LogActionFilter>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// Default → Login page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();