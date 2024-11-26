using Muflone.Core;

namespace BrewUp.Shared.CustomTypes;

//public record Price(decimal Value, string Currency);


public class Price(decimal value, string currency) : ValueObject
{
	public readonly decimal Value = value;
	public readonly string Currency = currency ?? throw new NotImplementedException(nameof(currency));

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
		yield return Currency;
	}
}
