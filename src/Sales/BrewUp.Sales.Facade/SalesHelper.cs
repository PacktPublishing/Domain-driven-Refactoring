using BrewUp.Infrastructure.RabbitMq;
using BrewUp.Sales.Facade.Validators;
using BrewUp.Sales.Infrastructures.MongoDb;
using BrewUp.Sales.Infrastructures.RabbitMq;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ReadModel;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Facade;

public static class SalesHelper
{
	public static IServiceCollection AddSales(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SalesOrderContractValidator>();
		services.AddSingleton<ValidationHandler>();

		services.AddScoped<ISalesFacade, SalesFacade>();
		services.AddScoped<ISalesOrderService, SalesOrderService>();
		services.AddScoped<IAvailabilityService, AvailabilityService>();
		services.AddScoped<IQueries<SalesOrder>, SalesOrderQueries>();
		services.AddScoped<IQueries<Availability>, AvailabilityQueries>();

		return services;
	}

	public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services, RabbitMqSettings rabbitMqSettings)
	{
		services.AddSalesMongoDb();
		services.AddRabbitMqForSalesModule(rabbitMqSettings);

		return services;
	}
}