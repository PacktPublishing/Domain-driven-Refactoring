using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.ReadModel.Dtos;

public class SalesOrder : DtoBase
{
	public string SalesOrderNumber { get; private set; } = new(string.Empty);
	public DateTime OrderDate { get; private set; } = DateTime.MinValue;

	public Guid CustomerId { get; private set; } = Guid.Empty;
	public string CustomerName { get; private set; } = string.Empty;

	public IEnumerable<SalesOrderRow> Rows { get; private set; } = Enumerable.Empty<SalesOrderRow>();

	protected SalesOrder()
	{
	}

	public static SalesOrder Create(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate, CustomerId customerId,
		CustomerName customerName, IEnumerable<SalesOrderRow> rows)
	{
		return new SalesOrder
		{
			Id = salesOrderId.Value.ToString(),
			SalesOrderNumber = salesOrderNumber.Value,
			OrderDate = orderDate.Value,

			CustomerId = customerId.Value,
			CustomerName = customerName.Value,

			Rows = rows
		};
	}

	public SalesOrderJson ToJson() =>	new(Id, SalesOrderNumber, CustomerId, CustomerName, OrderDate, Rows.Select(r => r.ToJson()));

}

