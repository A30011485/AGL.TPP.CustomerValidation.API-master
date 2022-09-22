using Xunit;

namespace AGL.TPP.CustomerValidation.Endpoint.Tests.Fixtures
{
    public struct FixtureCollections
    {
        public const string TestServerFixtureCollection = "TestServerFixtureCollection";
    }

    [CollectionDefinition(FixtureCollections.TestServerFixtureCollection)]
    public class TestServerFixtureCollection : ICollectionFixture<TestServerFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}