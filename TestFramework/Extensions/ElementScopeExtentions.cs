using System;
using Coypu;
using Coypu.Timing;
using OpenQA.Selenium;
using TestFramework.Locators;
using TestFramework.Pages;

namespace TestFramework.Extensions
{
    public static class ElementScopeExtentions
    {
        /// <summary>
        /// <para>Find a page locator for a given page.</para>
        /// </summary>
        public static ElementScope Find<TPage>(this BrowserSession browser, Func<TPage, ILocator> getLocator) where TPage : class, IPage, new()
        {
            var page = new TPage();
            var locator = getLocator(page);
            return browser.Find(locator);
        }

        private static ElementScope Find(this Scope scope, ILocator locator)
        {
            ElementScope found;
            if (locator.Css.IsElementId())
            {
                found = GetElementById(scope, locator);
            } else if (locator.Css.IsElementClassName())
            {
                found = GetElementByClassName(scope, locator);
            }
            else
            {
                throw new Exception($"ERROR: Locator format was invalid. Locator Css: {locator.Css}");
            }
            return found;
        }

        private static ElementScope GetElementByClassName(Scope scope, ILocator locator)
        {
            try
            {
                return scope.FindCss(locator.Css);
            }
            catch (Exception)
            {

                throw new NotFoundException($"ERROR: Element was not found. {locator.Css}");
            }            
        }

        private static ElementScope GetElementById(Scope scope, ILocator locator)
        {
            try
            {
                return scope.FindId(locator.Css.Replace("#", ""));
            }
            catch (Exception)
            {
                throw new NotFoundException($"ERROR: Element was not found. {locator.Css}");
            }
        }
    }
}
