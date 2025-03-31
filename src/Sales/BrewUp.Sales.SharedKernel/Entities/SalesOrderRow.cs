using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;

namespace BrewUp.Sales.SharedKernel.Entities;

public class SalesOrderRow : EntityBase
{
	public Guid BeerId { get; set; }
	public string BeerName { get; set; }
	public Quantity Quantity { get; set; }
	public Price Price { get; set; }

	public SalesOrderRowJson ToJson()
	{
		return new()
		{
			BeerId = BeerId,
			BeerName = BeerName,
			Quantity = Quantity,
			Price = Price			
		};
	}
}