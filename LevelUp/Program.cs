using LevelUp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console().WriteTo.File("logs/levelup.txt", rollingInterval: RollingInterval.Day).CreateLogger();

try 
{
    Log.Information("Starting Level Up application");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<LevelUpContext>(
        options =>
            options
                .UseNpgsql(builder.Configuration["LEVELUP_DBCONNECTIONSTRING"])
                .UseSnakeCaseNamingConvention()
    );

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error/Error");
        app.UseHsts();
    }

    // Handles status codes like 404
    app.UseStatusCodePagesWithReExecute("/Error/Error", "?statusCode={0}");



    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


