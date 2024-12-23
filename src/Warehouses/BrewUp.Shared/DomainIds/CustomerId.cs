using Muflone.Core;

namespace BrewUp.Shared.DomainIds;

public sealed class CustomerId : DomainId
{
	public CustomerId(Guid value) : base(value)
	{
	}
}