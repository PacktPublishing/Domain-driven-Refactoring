using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Sales.Domain.Tests.Entities;

public sealed class CreateSalesOrderSuccessfully : CommandSpecification<CreateSalesOrder>
{
	private readonly SalesOrderId _salesOrderId = new(Guid.NewGuid());
	private readonly SalesOrderNumber _salesOrderNumber = new("20240315-1500");
	private readonly OrderDate _orderDate = new(DateTime.UtcNow);

	private readonly Guid _correlationId = Guid.NewGuid();

	private readonly CustomerId _customerId = new(Guid.NewGuid());
	private readonly CustomerName _customerName = new("Muflone");

	private readonly IEnumerable<SalesOrderRowJson> _rows = Enumerable.Empty<SalesOrderRowJson>();

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreateSalesOrder When()
	{
		return new CreateSalesOrder(_salesOrderId, _correlationId, _salesOrderNumber, _orderDate, _customerId, _customerName, _rows);
	}

	protected override ICommandHandlerAsync<CreateSalesOrder> OnHandler()
	{
		return new CreateSalesOrderCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new SalesOrderCreated(_salesOrderId, _correlationId, _salesOrderNumber, _orderDate, _customerId, _customerName, _rows);
	}
}