using BrewUp.Mediator.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Mediator;

public static class MediatorHelper
{
	public static IServiceCollection AddMediator(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SalesOrderValidator>();
		services.AddSingleton<ValidationHandler>();

		services.AddScoped<IBrewUpMediator, BrewUpMediator>();

		return services;
	}
}