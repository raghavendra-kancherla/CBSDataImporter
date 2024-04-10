using CBSDataImporter;
using CBSDataImporter.Repositories;
using CBSDataImporter.Repositories.Interfaces;
using CBSDataImporter.Services;
using CBSDataImporter.Services.Interfaces;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


public class Program
{
	public static void Main(string[] args)
	{

		var provider = ConfigureServices();

		var app = new CommandLineApplication<Application>();
		app.Conventions
			.UseDefaultConventions()
			.UseConstructorInjection(provider);

		app.Execute(args);
	}
	public static ServiceProvider ConfigureServices()
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			 .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);

		var config = configuration.Build();

		var services = new ServiceCollection();
		services.AddSingleton<IConfiguration>(config);
		services.AddTransient<ICRMRepository, CRMRepository>();
		services.AddTransient<ICBSRepository, CBSRepository>();

		return services.BuildServiceProvider();
	}
}

