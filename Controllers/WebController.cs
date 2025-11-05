/*
 * Template: Dotnet HTTP Server Template
 * Version: 1.0.0
 * Author  : Aaron Fredrick
 * File    : WebController.cs
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace DotnetHttpServerTemplate.Controllers
{
	public static class WebController
	{
		// Map web routes
		public static void MapRoutes(WebApplication app)
		{
			// Serve static files under /static
			app.UseStaticFiles(
				new StaticFileOptions
				{
					FileProvider = new PhysicalFileProvider(
						Path.Combine(Directory.GetCurrentDirectory(), "static")
					),
					RequestPath = "/static",
				}
			);

			// HTML page routes
			app.MapGet("/", async context => await ServeHtml(context, "index.html"));
			app.MapGet("/about", async context => await ServeHtml(context, "about.html"));
		}

		// Helper to serve HTML pages from templates folder
		private static async Task ServeHtml(HttpContext context, string page)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "templates", page);

			if (File.Exists(filePath))
			{
				context.Response.ContentType = "text/html";
				await context.Response.SendFileAsync(filePath);
			}
			else
			{
				context.Response.StatusCode = 404;
				await context.Response.WriteAsync("<h1>404 - Page Not Found</h1>");
			}
		}
	}
}
