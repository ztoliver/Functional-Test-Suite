namespace SiteFunctionalTests.FunctionalTests
{
    public interface ITestBase
    {
        void SetUp();
        void TearDown();
        void SetUpTest();
        void TearDownTest();
    }
}