using System;
using System.Threading.Tasks;
using Coypu;
using NUnit.Framework;
using TestFramework.Pages;

namespace TestFramework.Extensions
{
    public static class BrowserExtentions
    {
        #region Visit()
        public static BrowserSession VisitPage<TPage>(this BrowserSession browser) where TPage : class, IPage, new()
        {
            var page = new TPage();
            Console.WriteLine($"Visiting {page.Name} <{page.Url}>...");
            browser.Visit(page.Url);
            Assert.That(browser.AbsoluteUri(), Is.EqualTo(page.Url), $"Page is not at {page.Url}");
            Console.WriteLine($"Successfully visited {page.Name} <{page.Url}>...");
            return browser;
        }
        public static BrowserSession GoToHomePage(this BrowserSession browser)
        {
            browser.VisitPage<HomePage>();
            return browser;
        }

        public static BrowserSession GoToResumePage(this BrowserSession browser)
        {
            browser.VisitPage<ResumePage>();
            return browser;
        }
        #endregion

        public static BrowserSession FillOutContactForm(this BrowserSession browser)
        {
            Parallel.Invoke(
                () => { browser.FindField("First Name").FillInWith("Zachary"); },
                () => { browser.FindField("Last Name").FillInWith("Toliver"); },
                () => { browser.FindField("Email").FillInWith("zt@itest.com"); },
                () => { browser.FindField("Message").FillInWith("This is a functional test run"); }
            );
            return browser;
        }

        public static string AbsoluteUri(this BrowserSession browser)
        {
            return browser.Location.AbsoluteUri;
        }

        #region Click Extensions()
        public static BrowserSession ClickHeaderNameLink(this BrowserSession browser)
        {
            browser.FindCss(".brand-logo").Click();
            return browser;
        }

        public static BrowserSession ClickSubmitButton(this BrowserSession browser)
        {
            browser.FindButton("Submit").Click();
            return browser;
        }
        #endregion
    }
}
