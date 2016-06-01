using System.Diagnostics;
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
            Browser.GoToHomePage();
            Assert.That(Browser.Title, Is.EqualTo("Zachary Toliver | Home"), "Page title is not correct");
        }

        [Test]
        public void Page_location_is_correct()
        {
            Browser.GoToHomePage();
            Assert.That(Browser.AbsoluteUri(), Is.EqualTo("http://zacharytoliver.com/"), "Page location is not correct");
        }

        [Test]
        public void Can_fill_out_contact_form()
        {
            Browser.GoToHomePage();
            Browser.FillOutContactForm();
            Browser.ClickSubmitButton();
            var contactForm = Browser.FindId("contactform");
            Thread.Sleep(1000);
            Assert.That(contactForm.Text, Is.EqualTo("Thank you for your message! I will respond as soon as possible."));
        }

    }
}
