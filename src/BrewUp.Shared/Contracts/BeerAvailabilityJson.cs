using BrewUp.Shared.CustomTypes;

namespace BrewUp.Shared.Contracts;

public record BeerAvailabilityJson(string BeerId, string BeerName, Availability Availability);