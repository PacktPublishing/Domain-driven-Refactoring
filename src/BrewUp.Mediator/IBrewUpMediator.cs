using BrewUp.Shared.Contracts;

namespace BrewUp.Mediator;

public interface IBrewUpMediator
{
	Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken);
}