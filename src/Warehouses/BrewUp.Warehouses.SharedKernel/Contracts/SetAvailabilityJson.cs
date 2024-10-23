using BrewUp.Shared.CustomTypes;

namespace BrewUp.Warehouses.SharedKernel.Contracts;

public record SetAvailabilityJson(string BeerId, string BeerName, Quantity Quantity);