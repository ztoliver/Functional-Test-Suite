using NUnit.Framework;
using Shouldly;
using TestFramework.Locators;

namespace TestFrameworkFunctionalTests.Locators
{
    [TestFixture]
    public class CssLocatorTests
    {
        private CssLocator ContactForm => new CssLocator().WithId("contactform").WithName("ContactForm");

        [Test]
        public void CssLocator_WhenInvoked_SetLocatorNameToTypeName()
        {
            //Arrange
            var locatorName = ContactForm.Name;

            //Assert
            locatorName.ShouldBe("ContactForm");
        }

        [Test]
        public void WithId_WhenInvoked_ShouldPrependHashToLocatorString()
        {
            //Arrange
            var locator = "locator";

            //Act
            var csslLocator = new CssLocator().WithId(locator);

            //Assert
            csslLocator.Css.ShouldBe("#locator");
        }

        [Test]
        public void WithClassName_WhenInvoked_ShouldPrependHashToLocatorString()
        {
            //Arrange
            var locator = "locator";

            //Act
            var csslLocator = new CssLocator().WithClassName(locator);

            //Assert
            csslLocator.Css.ShouldBe(".locator");
        }
    }
}
