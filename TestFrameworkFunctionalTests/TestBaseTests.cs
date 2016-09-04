using FakeItEasy;
using NUnit.Framework;
using SiteFunctionalTests.FunctionalTests;

namespace TestFrameworkFunctionalTests
{
    [TestFixture]
    public class TestBaseTests
    {
        [Test]
        [Category("TestFramework")]
        public void fsdfdsf()
        {
            var x = A.Fake<ITestBase>();
            x.SetUpTest();
            A.CallTo(() => x.SetUpTest()).MustHaveHappened();
        }
    }
}
