using BrewUp.Shared.Contracts;
using FluentValidation;

namespace BrewUp.Mediator.Validators;

public class SalesOrderValidator : AbstractValidator<SalesOrderJson>
{
	public SalesOrderValidator()
	{
		RuleFor(v => v.SalesOrderNumber).NotEmpty();
		RuleFor(v => v.OrderDate).GreaterThan(DateTime.MinValue);

		RuleFor(v => v.CustomerId).NotEqual(Guid.Empty);
		RuleFor(v => v.CustomerName).NotEmpty();

		RuleForEach(v => v.Rows).SetValidator(new SalesOrderRowValidator());
	}
}