/*
 * Template: Dotnet HTTP Server Template
 * Version: 1.0.0-alpha
 * Author  : Aaron Fredrick
 * File    : WebController.cs
 */


using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Dynamic;
using RazorLight;

namespace DotnetHttpServerTemplate.Controllers
{
	public static class WebController
	{
		private static readonly RazorLightEngine _razorEngine =
			new RazorLightEngineBuilder()
				.UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Views"))
				.UseMemoryCachingProvider()
				.Build();

		// Dynamic model accessible across all routes
		private static readonly dynamic _model = new ExpandoObject();

		static WebController()
		{
			// Populate default properties from assembly
			var assembly = Assembly.GetExecutingAssembly();
			var infoVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "0.0.0";
			var plusIndex = infoVersion.IndexOf('+');
			_model.Version = infoVersion[..(plusIndex >= 0 ? plusIndex : infoVersion.Length)];
			_model.Author = "Aaron Fredrick"; // or read from assembly attribute
		}

		// Add or update a property dynamically
		public static void SetProperty(string key, object value)
		{
			((IDictionary<string, object>)_model)[key] = value;
		}

		private static async Task RenderPage(HttpContext context, string template = "index.cshtml", object? model = null)
		{
			model ??= _model; // Use default model if none provided
			var html = await _razorEngine.CompileRenderAsync(template, model);
			context.Response.ContentType = "text/html";
			await context.Response.WriteAsync((string)html);
		}

		public static void MapRoutes(WebApplication app)
		{
			// Serve static files
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
						Path.Combine(Directory.GetCurrentDirectory(), "static")
					),
				RequestPath = "/static",
			});

			// Render Razor templates
			app.MapGet("/", async context => await RenderPage(context));
			app.MapGet("/about", async context => await RenderPage(context, "about.cshtml"));
		}
	}
}

