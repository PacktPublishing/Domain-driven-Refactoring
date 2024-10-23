using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BrewUp.Sales.Facade;
using NetArchTest.Rules;

namespace BrewUp.Sales.Architecture.Tests;

[ExcludeFromCodeCoverage]
public class SalesArchitectureTests
{
	[Fact]
	public void Should_SalesArchitecture_BeCompliant()
	{
		var types = Types.InAssembly(typeof(SalesFacade).Assembly);

		var forbiddenAssemblies = new List<string>
		{
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
    
	[Fact]
	public void SalesProjects_Should_Having_Namespace_StartingWith_BrewUp_Sales()
	{
		var salesModulePath = Path.Combine(VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName, "Sales");
		var subFolders = Directory.GetDirectories(salesModulePath);

		var netVersion = Environment.Version;

		var salesAssemblies = (from folder in subFolders
								   let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
								   let files = Directory.GetFiles(binFolder)
								   let folderArray = folder.Split(Path.DirectorySeparatorChar)
								   select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
			into assemblyFilename
								   where !assemblyFilename!.Contains("Test")
								   select Assembly.LoadFile(assemblyFilename!)).ToList();

		var salesTypes = Types.InAssemblies(salesAssemblies);
		var salesResult = salesTypes
			.Should()
			.ResideInNamespaceStartingWith("BrewUp.Sales")
			.GetResult();

		Assert.True(salesResult.IsSuccessful);
	}

	private static class VisualStudioProvider
	{
		public static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
		{
			var directory = new DirectoryInfo(
				currentPath ?? Directory.GetCurrentDirectory());
			while (directory != null && !directory.GetFiles("*.sln").Any())
			{
				directory = directory.Parent;
			}
			return directory;
		}
	}
}