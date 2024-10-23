using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.ReadModel;

namespace BrewUp.Sales.ReadModel.Dtos;

public class SalesOrderRow : DtoBase
{
	public Guid BeerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public string BeerName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Quantity Quantity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Price Price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public SalesOrderRowJson ToJson()
	{
		throw new NotImplementedException();
	}
}