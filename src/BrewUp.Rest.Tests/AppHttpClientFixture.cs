using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BrewUp.Rest.Tests;

public class AppHttpClientFixture
{
	public readonly HttpClient Client;

	public AppHttpClientFixture()
	{
		var app = new ProjectsApplication();
		Client = app.CreateClient();
	
	}

	private class ProjectsApplication : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			builder.ConfigureHostConfiguration(config =>
			{
				// Overriding a configuration key
				config.AddJsonFile("appsettings-tests.json", optional: false, reloadOnChange: true);
			});
			
			builder.ConfigureServices((_, services) =>
			{
				services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

				Log.Logger = new LoggerConfiguration()
					.WriteTo.File("Logs\\BrewUp.Rest.Tests.log")
					.CreateLogger();
			});
		
			return base.CreateHost(builder);
		}
	}
}