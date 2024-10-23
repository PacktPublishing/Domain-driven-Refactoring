using BrewUp.Shared.CustomTypes;

namespace BrewUp.Shared.Contracts;

public record SetAvailabilityJson(string BeerId, string BeerName, Quantity Quantity);