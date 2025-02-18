using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.CreateLogger();

try
{
	Log.Information("application start...");
	var builder = WebApplication.CreateBuilder(args);
	builder.Services.AddSerilog();
	builder.Services.AddControllersWithViews();

	var app = builder.Build();
	app.UseStaticFiles();
	app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "application terminated unexpectedly.");
}
finally
{
	Log.CloseAndFlush();
}
