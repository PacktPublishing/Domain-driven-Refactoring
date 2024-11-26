using BrewUp.Sales.Domain.Helpers;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Core;

namespace BrewUp.Sales.Domain.Entities;

public class SalesOrder : AggregateRoot
{
	internal SalesOrderNumber _salesOrderNumber;
	internal OrderDate _orderDate;

	internal CustomerId _customerId;
	internal CustomerName _customerName;

	internal IEnumerable<SalesOrderRow> _rows;

	protected SalesOrder()
	{
	}

	internal static SalesOrder CreateSalesOrder(SalesOrderId salesOrderId, Guid correlationId, SalesOrderNumber salesOrderNumber,
		OrderDate orderDate, CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowDto> rows)
	{
		// Check SalesOrder invariants

		return new SalesOrder(salesOrderId, correlationId, salesOrderNumber, orderDate, customerId, customerName, rows);
	}

	private SalesOrder(SalesOrderId salesOrderId, Guid correlationId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowDto> rows)
	{		
		RaiseEvent(new SalesOrderCreated(salesOrderId, correlationId, salesOrderNumber, orderDate, customerId, customerName, rows));
	}

	private void Apply(SalesOrderCreated @event)
	{
		Id = @event.SalesOrderId;
		_salesOrderNumber = @event.SalesOrderNumber;
		_orderDate = @event.OrderDate;
		_customerId = @event.CustomerId;
		_customerName = @event.CustomerName;
		_rows = @event.Rows.MapToDomainRows();
	}
}