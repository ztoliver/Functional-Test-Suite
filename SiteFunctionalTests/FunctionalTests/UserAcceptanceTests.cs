using System.Diagnostics;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using TestFramework.Extensions;

namespace SiteFunctionalTests.FunctionalTests
{
    [TestFixture]
    public class UserAcceptanceTests : TestBase
    {
        [Test]
        public void Title_is_correct()
        {
            ExecuteBrowserTest(browser =>
            {
                browser.GoToHomePage();
                Assert.That(browser.Title, Is.EqualTo("Zachary Toliver | Home"), "Page title is not correct");
            });
        }

        [Test]
        public void Page_location_is_correct()
        {
            ExecuteBrowserTest(browser =>
            {
                Browser.GoToHomePage();
                Assert.That(Browser.AbsoluteUri(), Is.EqualTo("http://zacharytoliver.com/"), "Page location is not correct");
            });
        }

        [Test]
        public void Can_fill_out_contact_form()
        {
            ExecuteBrowserTest(browser =>
            {
                Browser.GoToHomePage();
                Browser.FillOutContactForm();
                Browser.ClickSubmitButton();
                var contactForm = Browser.FindId("contactform");
                Thread.Sleep(1000);
                Assert.That(contactForm.Text, Is.EqualTo("Thank you for your message! I will respond as soon as possible."));
            });
        }

        [Test]
        public void Cannot_leave_out_first_name_in_contact_form()
        {
            ExecuteBrowserTest(browser =>
            {
                Browser.GoToHomePage();
                Browser.FillOutContactForm();
                Browser.FindField("First Name").FillInWith("");
                Browser.ClickSubmitButton();
                Thread.Sleep(1000);
                var failureMessage = Browser.FindCss(".failure-message");
                Assert.That(failureMessage.Text, Is.EqualTo("First name is required."));
            });
        }

        [Test]
        public void Cannot_leave_out_last_name_in_contact_form()
        {
            ExecuteBrowserTest(browser =>
            {
                Browser.GoToHomePage();
                Browser.FillOutContactForm();
                Browser.FindField("Last Name").FillInWith("");
                Browser.ClickSubmitButton();
                Thread.Sleep(1000);
                var failureMessage = Browser.FindCss(".failure-message");
                Assert.That(failureMessage.Text, Is.EqualTo("Last name is required."));
            });
        }

        [Test]
        public void Cannot_leave_out_email_address_in_contact_form()
        {
            ExecuteBrowserTest(browser =>
            {
                Browser.GoToHomePage();
                Browser.FillOutContactForm();
                Browser.FindField("Email").FillInWith("");
                Browser.ClickSubmitButton();
                Thread.Sleep(1000);
                var failureMessage = Browser.FindCss(".failure-message");
                Assert.That(failureMessage.Text, Is.EqualTo("Email address is required."));
            });
        }

        [Test]
        public void Cannot_leave_contact_form_empty()
        {
            ExecuteBrowserTest(browser =>
            {
                Browser.GoToHomePage();
                Browser.ClickSubmitButton();
                Thread.Sleep(1000);
                var failureMessages = Browser.FindAllCss(".failure-message").ToList();
                Assert.That(failureMessages[0].Text.Contains("First name is required") ,"First name failure message not shown.");
                Assert.That(failureMessages[1].Text.Contains("Last name is required"));
                Assert.That(failureMessages[2].Text.Contains("Email address is required"));
                Assert.That(failureMessages[3].Text.Contains("Message content is required"));
            });
        }
    }
}
