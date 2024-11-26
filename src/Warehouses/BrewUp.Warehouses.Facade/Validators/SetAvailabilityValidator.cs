using BrewUp.Warehouses.SharedKernel.Contracts;
using FluentValidation;

namespace BrewUp.Warehouses.Facade.Validators;

public sealed class SetAvailabilityValidator : AbstractValidator<SetAvailabilityJson>
{
	public SetAvailabilityValidator()
	{
		RuleFor(x => x.BeerId).NotEmpty();
		RuleFor(x => x.BeerName).NotEmpty();
		RuleFor(x => x.Quantity.Value).GreaterThan(0);
		RuleFor(x => x.Quantity.UnitOfMeasure).NotEmpty();
	}
}