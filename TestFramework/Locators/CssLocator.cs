using TestFramework.Extensions;

namespace TestFramework.Locators
{
    public class CssLocator : ILocator
    {
        public string Name => GetElementName();

        public string Css { get; set; }

        ///<summary>
        ///<para>Use an element id to create a page locator</para>
        ///</summary>
        public CssLocator WithId(string id)
        {
            if (id.IsElementId()) { Css = id; return this; }
            Css = $"#{id}";
            return this;
        }

        ///<summary>
        ///<para>Use an element class nameto create a page locator</para>
        ///</summary>
        public CssLocator WithClassName(string className)
        {
            if (className.IsElementClassName()) { Css = className; return this; }
            Css = $"#{className}";
            return this;
        }

        private string GetElementName()
        {
            return GetType().Name;
        }
    }
}
