namespace BrewUp.Shared.Contracts;

public class BeerJson
{
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;
	public string BeerType { get; set; } = string.Empty;
	public bool HomeBrewed { get; set; }
}