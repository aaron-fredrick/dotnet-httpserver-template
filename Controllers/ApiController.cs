/*
 * Template: Dotnet HTTP Server Template
 * Version: 1.0.0
 * Author  : Aaron Fredrick
 * File    : ApiController.cs
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DotnetHttpServerTemplate.Controllers
{
	public static class ApiController
	{
		// Map API routes
		public static void MapRoutes(WebApplication app)
		{
			// GET /api/status → returns server status and time
			app.MapGet("/api/status", () =>
			{
				return Results.Json(new { status = "running", serverTime = DateTime.UtcNow });
			});

			// POST /api/echo → echoes request body
			app.MapPost("/api/echo", async (HttpContext context) =>
			{
				using var reader = new StreamReader(context.Request.Body);
				var body = await reader.ReadToEndAsync();
				return Results.Json(new { received = body });
			});
		}
	}
}
