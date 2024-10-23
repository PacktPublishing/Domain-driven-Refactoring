using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BrewUp.Warehouses.Facade;
using NetArchTest.Rules;

namespace BrewUp.Warehouses.Architecture.Tests;

[ExcludeFromCodeCoverage]
public class WarehousesArchitectureTests
{
	[Fact]
	public void Should_WarehousesArchitecture_BeCompliant()
	{
		var types = Types.InAssembly(typeof(WarehousesFacade).Assembly);

		var forbiddenAssemblies = new List<string>
		{
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
    
	[Fact]
	public void WarehousesProjects_Should_Having_Namespace_StartingWith_BrewUp_Warehouses()
	{
		var warehousesModulePath = Path.Combine(VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName, "Warehouses");
		var subFolders = Directory.GetDirectories(warehousesModulePath);

		var netVersion = Environment.Version;

		var warehousesAssemblies = (from folder in subFolders
								   let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
								   let files = Directory.GetFiles(binFolder)
								   let folderArray = folder.Split(Path.DirectorySeparatorChar)
								   select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
			into assemblyFilename
								   where !assemblyFilename!.Contains("Test")
								   select Assembly.LoadFile(assemblyFilename!)).ToList();

		var warehousesTypes = Types.InAssemblies(warehousesAssemblies);
		var warehousesResult = warehousesTypes
			.Should()
			.ResideInNamespaceStartingWith("BrewUp.Warehouses")
			.GetResult();

		Assert.True(warehousesResult.IsSuccessful);
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