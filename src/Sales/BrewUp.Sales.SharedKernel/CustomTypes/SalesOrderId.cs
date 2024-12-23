using Muflone.Core;

namespace BrewUp.Sales.SharedKernel.CustomTypes;

public sealed class SalesOrderId : DomainId
{
	public SalesOrderId(Guid value) : base(value)
	{
	}
}