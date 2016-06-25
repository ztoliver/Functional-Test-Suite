using System;
using System.Diagnostics;
using System.IO;
using Coypu;
using Coypu.Drivers.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Configuration;
using System.Linq;
using NUnit.Framework.Interfaces;

namespace SiteFunctionalTests.FunctionalTests
{
    public class TestBase : ITestBase
    {
        public BrowserSession Browser;
        public static string TestArtifacts;
        private static string _saveDirectory;
        private static string _saveLocation;
        private static string _htmlReport;

        public TestBase()
        {
            _saveDirectory = ConfigurationManager.AppSettings["ArtifactFilePath"];
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug", "");
            var className = TestContext.CurrentContext.Test.ClassName.Split(Convert.ToChar(".")).Last();
            _saveLocation = $"{workingDirectory}\\{className}";
            _htmlReport = workingDirectory + "\\artifact-report.html";
        }

        [SetUp]
        public void SetUpTest()
        {
            Console.WriteLine("Starting test set up...");
        }

        [TearDown]
        public void TearDownTest()
        {
            Console.WriteLine("Starting test tear down...");
            GenerateSuccessArtifacts();
        }

        [OneTimeSetUp]
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

        [OneTimeTearDown]
        public void TearDown()
        {
            Debug.WriteLine("Tearing down test fixture...");
            GenerateFullTestFixtureArtifact();
            Browser.Dispose();
        }

        private static void GenerateSuccessArtifacts()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                GeneratePassedArtifact();
            }
        }

        private static void GenerateFailedArtifact(Exception ex, string screenshot)
        {
            Console.WriteLine("Creating test failure artifact...");
            var failedTemplate = "<li>" +
                                       $"<div class=\"collapsible-header test-header-failed red lighten-2 active\">{TestContext.CurrentContext.Test.Name}</div>" +
                                       "<div class=\"collapsible-body test-body-failed red lighten-3\">" +
                                            $"<img class=\"responsive-img\" src=\"data:image/jpeg;base64,{screenshot}\"/>" +
                                            $"<h4>Reason:</h4><p>{ex.Message}</p><h4>Stacktrace:</h4><p>{ex.StackTrace}</p>" +
                                       "</div>" +
                                  "</li>";
            TestArtifacts = TestArtifacts + failedTemplate;
            Console.WriteLine("Test failure artifact created...");
        }

        private static void GeneratePassedArtifact()
        {
            Console.WriteLine("Generating test success artifact...");
            var passedTemplate = "<li>" +
                                 $"<div class=\"collapsible-header green lighten-2 test-header-passed\">{TestContext.CurrentContext.Test.Name}</div>" +
                                 "</li>";
            TestArtifacts = TestArtifacts + passedTemplate;
            Console.WriteLine("Test success artifact created...");
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
            var html = File.ReadAllText(_htmlReport).Replace("{0}", TestArtifacts);
            try
            {
                if (Directory.Exists(_saveLocation))
                {
                    File.WriteAllText($"{_saveLocation}/artifacts.html", html);
                }
                else
                {
                    Directory.CreateDirectory(_saveLocation);
                    File.WriteAllText($"{_saveLocation}/artifacts.html", html);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Error: Unable to save test fixture artifact");
            }
        }

        protected void ExecuteBrowserTest(Action<BrowserSession> test)
        {
            try
            {
                Console.WriteLine("Executing test...");
                test(Browser);
            }
            catch (Exception ex)
            {
                var screenshot = ((ITakesScreenshot)Browser.Driver.Native).GetScreenshot().AsBase64EncodedString;
                switch (ex.TargetSite.Name)
                {
                    case "Inconclusive":
                        Console.WriteLine("Test was inconclusive!");
                        GenerateIgnoredArtifact();
                        break;
                    case "Ignore":
                        Console.WriteLine("Test was ignored!");
                        GenerateIgnoredArtifact();
                        break;
                    case "Success":
                        Console.WriteLine("Test passed!");
                        GeneratePassedArtifact();
                        break;
                    case "Fail":
                        Console.WriteLine("Failure caught in browser test. Test will now tear down... \n" + ex);
                        GenerateFailedArtifact(ex, screenshot);
                        break;
                    default:
                        Console.WriteLine("Failure caught in browser test. Test will now tear down... \n" + ex);
                        GenerateFailedArtifact(ex, screenshot);
                        break;
                }
                throw;
            }
        }
    }
}