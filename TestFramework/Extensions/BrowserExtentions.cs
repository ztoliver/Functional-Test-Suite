using Coypu;

namespace TestFramework.Extensions
{
    public static class BrowserExtentions
    {
        public static BrowserSession GoToHomePage(this BrowserSession browser)
        {
            browser.Visit("http://zacharytoliver.com/");
            return browser;
        }

        public static BrowserSession FillOutContactForm(this BrowserSession browser)
        {
            browser.FindField("First Name").FillInWith("Zachary");
            browser.FindField("Last Name").FillInWith("Toliver");
            browser.FindField("Email").FillInWith("zt@itest.com");
            browser.FindField("Message").FillInWith("This is a functional test run");
            return browser;
        }

        public static BrowserSession ClickSubmitButton(this BrowserSession browser)
        {
            browser.FindButton("Submit").Click();
            return browser;
        }

        public static string AbsoluteUri(this BrowserSession browser)
        {
            return browser.Location.AbsoluteUri;
        }
    }
}
