namespace BrewUp.Shared.Contracts;

public class WarehouseDto
{
	public Guid WarehouseId { get; set; } = Guid.Empty;
	public string WarehouseName { get; set; } = string.Empty;
}