namespace BrewUp.Shared.Contracts;

public class WarehouseJson
{
	public Guid WarehouseId { get; set; } = Guid.Empty;
	public string WarehouseName { get; set; } = string.Empty;
}