using System.Diagnostics.CodeAnalysis;
using NetArchTest.Rules;

namespace BrewUp.Mediator.Tests;

[ExcludeFromCodeCoverage]
public class MediatorFitnessTests
{
    [Fact]
    public void Should_MediatorArchitecture_BeCompliant()
    {
        var types = Types.InAssembly(typeof(BrewUpMediator).Assembly);

        var forbiddenAssemblies = new List<string>
        {
            "BrewUp.Warehouses.Domain",
            "BrewUp.Warehouses.Infrastructures",
            "BrewUp.Warehouses.ReadModel",
            "BrewUp.Warehouses.SharedKernel",
            "BrewUp.Sales.Domain",
            "BrewUp.Sales.Infrastructures",
            "BrewUp.Sales.ReadModel",
            "BrewUp.Sales.SharedKernel"
        };

        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(forbiddenAssemblies.ToArray())
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
}