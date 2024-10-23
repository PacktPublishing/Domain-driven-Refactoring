using BrewUp.Sales.Domain.Helper;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;

namespace BrewUp.Sales.Domain.Entities;

public class SalesOrder : AggregateRoot
{
	internal readonly SalesOrderId _salesOrderId;
	internal readonly SalesOrderNumber _salesOrderNumber;
	internal readonly OrderDate _orderDate;

	internal readonly CustomerId _customerId;
	internal readonly CustomerName _customerName;

	internal readonly IEnumerable<SalesOrderRow> _rows;

	protected SalesOrder()
	{
	}

	internal static SalesOrder CreateSalesOrder(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber,
		OrderDate orderDate, CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowJson> rows)
	{
		return new SalesOrder(salesOrderId, salesOrderNumber, orderDate, customerId, customerName, rows.MapToDomainRows());
	}

	private SalesOrder(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRow> row)
	{
		_salesOrderId = salesOrderId;
		_salesOrderNumber = salesOrderNumber;
		_orderDate = orderDate;

		_customerId = customerId;
		_customerName = customerName;

		_rows = row;
	}
}