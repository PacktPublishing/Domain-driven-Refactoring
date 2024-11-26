using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.SharedKernel.Contracts;

public record BeerAvailabilityJson(string BeerId, string BeerName, Quantity Quantity);