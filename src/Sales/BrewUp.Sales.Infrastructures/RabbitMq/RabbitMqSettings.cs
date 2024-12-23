namespace BrewUp.Sales.Infrastructures.RabbitMq;

public class RabbitMqSettings
{
	public string Host { get; set; } = string.Empty;
	public string ExchangeCommandName { get; set; } = string.Empty;
	public string ExchangeEventName { get; set; } = string.Empty;
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string ClientId { get; set; } = string.Empty;
}