/*
 * Template: Dotnet HTTP Server Template
 * Version: 1.0.0-alpha
 * Author : Aaron Fredrick
 * File   : Program.cs
 */

using DotnetHttpServerTemplate.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure console logging
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
	options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
	options.SingleLine = true;
	options.IncludeScopes = false;
});

// Get host and port from configuration or use defaults
var host = builder.Configuration.GetValue<string>("Server:Host") ?? "localhost";
var port = builder.Configuration.GetValue<int?>("Server:Port") ?? 5566;

var app = builder.Build();

// Log each request with duration
app.Use(async (context, next) =>
{
	var start = DateTime.UtcNow;
	await next.Invoke();
	var duration = DateTime.UtcNow - start;

	var log = $"{context.Request.Method} {context.Request.Path} " +
			  $"from {context.Connection.RemoteIpAddress} → {context.Response.StatusCode} " +
			  $"({duration.TotalMilliseconds:F1} ms)";
	app.Logger.LogInformation(log);
});

// Serve static files from "static" folder
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(Directory.GetCurrentDirectory(), "static")),
	RequestPath = "/static"
});

// Register controllers
WebController.MapRoutes(app);
ApiController.MapRoutes(app);

// Configure URLs and start server
app.Urls.Add($"http://{host}:{port}");
app.Logger.LogInformation("Server running on http://{Host}:{Port}", host, port);

app.Run();
