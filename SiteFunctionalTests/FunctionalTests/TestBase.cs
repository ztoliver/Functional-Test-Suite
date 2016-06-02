using System;
using System.Diagnostics;
using System.IO;
using Coypu;
using Coypu.Drivers.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Configuration;

namespace SiteFunctionalTests.FunctionalTests
{
    public class TestBase : ITestBase
    {
        public BrowserSession Browser;
        public static string TestArtifacts;
        private static string _saveDirectory;

        public TestBase()
        {
            _saveDirectory = ConfigurationManager.AppSettings["ArtifactFilePath"];
        }

        [SetUp]
        public void SetUpTest()
        {
            Debug.WriteLine("Test setup started");
        }

        [TearDown]
        public void TearDownTest()
        {
            Debug.WriteLine("Test tearing down");
            GenerateSuccessArtifacts();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            Debug.WriteLine("Setting up test fixture...");
            var sessionConfiguration = new SessionConfiguration
            {
                Driver = typeof(SeleniumWebDriver),
                Browser = Coypu.Drivers.Browser.Chrome
            };
            Browser = new BrowserSession(sessionConfiguration);
            Browser.MaximiseWindow();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Debug.WriteLine("Tearing down test fixture...");
            GenerateFullTestFixtureArtifact();
            Browser.Dispose();
        }

        private static void GenerateSuccessArtifacts()
        {
            if (TestContext.CurrentContext.Result.Status == TestStatus.Passed && TestContext.CurrentContext.Result.State == TestState.Success)
            {
                GeneratePassedArtifact();
            }
        }

        private static void GenerateFailedArtifact(Exception ex, string screenshot)
        {
            Debug.WriteLine("Creating failure artifact...");
            var failedTemplate = "<li>" +
                                       $"<div class=\"collapsible-header test-header-failed red lighten-2 active\">{TestContext.CurrentContext.Test.Name}</div>" +
                                       "<div class=\"collapsible-body test-body-failed red lighten-3\">" +
                                            $"<img class=\"responsive-img\" src=\"data:image/jpeg;base64,{screenshot}\"/>" +
                                            $"<h4>Reason:</h4><p>{ex.Message}</p><h4>Stacktrace:</h4><p>{ex.StackTrace}</p>" +
                                       "</div>" +
                                  "</li>";
            TestArtifacts = TestArtifacts + failedTemplate;
        }

        private static void GeneratePassedArtifact()
        {
            Debug.WriteLine("Creating success artifact...");
            var passedTemplate = "<li>" +
                                 $"<div class=\"collapsible-header green lighten-2 test-header-passed\">{TestContext.CurrentContext.Test.Name}</div>" +
                                 "</li>";
            TestArtifacts = TestArtifacts + passedTemplate;
        }

        private static void GenerateIgnoredArtifact()
        {
            var passedTemplate = "<li>" +
                                 $"<div class=\"collapsible-header yellow lighten-1 test-header-passed\">{TestContext.CurrentContext.Test.Name}</div>" +
                                 "</li>";
            TestArtifacts = TestArtifacts + passedTemplate;
        }

        private static void GenerateFullTestFixtureArtifact()
        {
            if (!Directory.Exists(_saveDirectory)) Directory.CreateDirectory(_saveDirectory);
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "");
            var saveLocation = workingDirectory + "\\artifacts.html";
            var htmlReport = workingDirectory + "\\artifact-report.html";
            var html = File.ReadAllText(htmlReport).Replace("{0}", TestArtifacts);
            File.WriteAllText(saveLocation, html);
        }

        protected void ExecuteBrowserTest(Action<BrowserSession> test)
        {
            try
            {
                Debug.WriteLine("Executing test...");
                test(Browser);
            }
            catch (Exception ex)
            {
                var screenshot = ((ITakesScreenshot)Browser.Driver.Native).GetScreenshot().AsBase64EncodedString;
                switch (ex.TargetSite.Name)
                {
                    case "Inconclusive":
                        Debug.WriteLine("Test was inconclusive!");
                        GenerateIgnoredArtifact();
                        break;
                    case "Ignore":
                        Debug.WriteLine("Test was ignored!");
                        GenerateIgnoredArtifact();
                        break;
                    case "Success":
                        Debug.WriteLine("Test passed!");
                        GeneratePassedArtifact();
                        break;
                    case "Fail":
                        Debug.WriteLine("Failure caught in browser test. Test will now tear down... \n" + ex);
                        GenerateFailedArtifact(ex, screenshot);
                        break;
                    default:
                        Debug.WriteLine("Failure caught in browser test. Test will now tear down... \n" + ex);
                        GenerateFailedArtifact(ex, screenshot);
                        break;
                }
                throw;
            }
        }
    }
}