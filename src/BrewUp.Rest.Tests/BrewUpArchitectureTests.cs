using NetArchTest.Rules;
using System.Diagnostics.CodeAnalysis;

namespace BrewUp.Rest.Tests;

[ExcludeFromCodeCoverage]
public class BrewUpArchitectureTests
{
	[Fact]
	public void Should_BrewUpArchitecture_BeCompliant()
	{
		var types = Types.InAssembly(typeof(Program).Assembly);

		var forbiddenAssemblies = new List<string>
		{
			"BrewUp.Sales.Domain",
			"BrewUp.Sales.Infrastructures",
			"BrewUp.Sales.ReadModel",
			"BrewUp.Sales.SharedKernel",
			"BrewUp.Warehouses.Domain",
			"BrewUp.Warehouses.Infrastructures",
			"BrewUp.Warehouses.ReadModel",
			"BrewUp.Warehouses.SharedKernel"
		};

		var result = types
			.ShouldNot()
			.HaveDependencyOnAny(forbiddenAssemblies.ToArray())
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
}