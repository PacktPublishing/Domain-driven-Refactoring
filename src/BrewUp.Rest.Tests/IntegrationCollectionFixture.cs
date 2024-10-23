using Xunit;

namespace BrewUp.Rest.Tests;

[CollectionDefinition("Integration Fixture")]
public abstract class IntegrationCollectionFixture : ICollectionFixture<AppHttpClientFixture>
{
}