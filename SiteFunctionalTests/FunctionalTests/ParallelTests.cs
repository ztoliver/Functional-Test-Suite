using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TestFramework.Extensions;
using TestFramework.Pages;

namespace SiteFunctionalTests.FunctionalTests
{
    [TestFixture]
    public class ParallelTests : TestBase
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
            Parallel.For(1, 5, x =>
            {
                ExecuteBrowserTest(browser =>
                {
                    Browser.GoToHomePage();
                    Assert.That(Browser.AbsoluteUri(), Is.EqualTo("http://zacharytoliver.com/"), "Page location is not correct");
                });
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
                var contactForm = browser.Find<HomePage>(x => x.ContactForm);
                Thread.Sleep(250);
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
                browser.GoToHomePage();
                browser.ClickSubmitButton();
                Thread.Sleep(1000);
                var failureMessages = browser.FindAllCss(".failure-message").ToList();
                Assert.That(failureMessages[0].Text.Contains("First name is required"), "First name failure message not shown.");
                Assert.That(failureMessages[1].Text.Contains("Last name is required"));
                Assert.That(failureMessages[2].Text.Contains("Email address is required"));
                Assert.That(failureMessages[3].Text.Contains("Message content is required"));
            });
        }

        [Test]
        public void Name_links_to_home_page()
        {
            ExecuteBrowserTest(browser =>
            {
                browser.GoToHomePage();
                browser.ClickHeaderNameLink();
            });
        }

        [Test]
        public void Main_image_is_present()
        {
            ExecuteBrowserTest(browser =>
            {
                browser.GoToHomePage();
                Assert.That(browser.Find<HomePage>(x => x.MainImage).Exists(), "Main image was not found");
            });
        }
    }
}
