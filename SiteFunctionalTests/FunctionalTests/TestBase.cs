using System.Diagnostics;
using System.Drawing.Imaging;
using Coypu;
using Coypu.Drivers.Selenium;
using NUnit.Framework;
using TestFramework.Extensions;

namespace SiteFunctionalTests.FunctionalTests
{
    public class TestBase : ITestBase
    {
        public BrowserSession Browser;

        [SetUp]
        public void SetUpTest()
        {
            Debug.WriteLine("Test setup started");
        }

        [TearDown]
        public void TearDownTest()
        {
            Debug.WriteLine("Test tearing down");
            Browser.SaveScreenshot($"{TestContext.CurrentContext.Test.FullName}.png", ImageFormat.Png);
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            Debug.WriteLine("Test class SetUp called");
            var sessionConfiguration = new SessionConfiguration
            {
                AppHost = "zacharytoliver.com",
                Port = 80,
                Driver = typeof(SeleniumWebDriver),
                Browser = Coypu.Drivers.Browser.Chrome
            };
            Browser = new BrowserSession(sessionConfiguration);
            Browser.MaximiseWindow();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Browser.Dispose();
        }
    }
}