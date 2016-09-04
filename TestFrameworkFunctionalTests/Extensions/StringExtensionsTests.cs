using System;
using NUnit.Framework;
using Shouldly;
using TestFramework.Extensions;

namespace TestFrameworkFunctionalTests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [Category("TestFramework")]
        public static void IsElementClassName_valid_element_classname_returns_true()
        {
            //Arrange
            const string validClassName = ".classname";

            //Act
            bool result = validClassName.IsElementClassName();

            //Assert
            result.ShouldBe(true);
        }

        [Test]
        [Category("TestFramework")]
        public static void IsElementClassName_invalid_element_classnames_return_false()
        {
            //Arrange
            const string invalidClassName1 = "classname";
            const string invalidClassName2 = "#classname";

            //Act
            bool result1 = invalidClassName1.IsElementClassName();
            bool result2 = invalidClassName2.IsElementClassName();

            //Assert
            result1.ShouldBe(false);
            result2.ShouldBe(false);
        }

        [Test]
        [Category("TestFramework")]
        public static void IsElementId_valid_element_classname_returns_true()
        {
            //Arrange
            const string validId = "#classname";

            //Act
            bool result = validId.IsElementId();

            //Assert
            result.ShouldBe(true);
        }

        [Test]
        [Category("TestFramework")]
        public static void IsElementId_invalid_element_classnames_return_false()
        {
            //Arrange
            const string invalidId1 = "classname";
            const string invalidId2 = ".classname";

            //Act
            bool result1 = invalidId1.IsElementId();
            bool result2 = invalidId2.IsElementId();

            //Assert
            result1.ShouldBe(false);
            result2.ShouldBe(false);
        }

        [Test]
        [Category("TestFramework")]
        public static void IsElementClassName_WhenClassNameIsNull_ThrowArgumentNullException()
        {
            //Arrange
            const string invalidClassName = null;

            //Act
            var delegateAction = new TestDelegate(delegate { invalidClassName.IsElementClassName(); });

            //Assert
            Assert.Throws<ArgumentNullException>(delegateAction);
        }

        [Test]
        [Category("TestFramework")]
        public static void IsElementId_WhenClassNameIsNull_ThrowArgumentNullException()
        {
            //Arrange
            const string invalidId = null;

            //Act
            var delegateAction = new TestDelegate(delegate { invalidId.IsElementId(); });

            //Assert
            Assert.Throws<ArgumentNullException>(delegateAction);
        }
    }
}
