using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.SharedKernel.Commands;

public class CreateSalesOrder(SalesOrderId aggregateId, Guid commitId, SalesOrderNumber salesOrderNumber,
		OrderDate orderDate, CustomerId customerId, CustomerName customerName,
		IEnumerable<SalesOrderRowJson> rows)
	: Command(aggregateId, commitId)
{
	public readonly SalesOrderId SalesOrderId = aggregateId;
	public readonly SalesOrderNumber SalesOrderNumber = salesOrderNumber;
	public readonly OrderDate OrderDate = orderDate;

	public readonly CustomerId CustomerId = customerId;
	public readonly CustomerName CustomerName = customerName;

	public readonly IEnumerable<SalesOrderRowJson> Rows = rows;
}