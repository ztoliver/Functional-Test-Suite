using System.Collections.Generic;
using System.Configuration;
using TestFramework.Locators;

namespace TestFramework.Pages
{
    public class HomePage : IPage
    {
        //Page Properties
        public string Name => "Home Page";
        public string Url => ConfigurationManager.AppSettings["HomePageUrl"];

        //Page Locators
        public CssLocator ContactForm => new CssLocator().WithId("contactform");
        public CssLocator MainImage => new CssLocator().WithId("main-img");
        public CssLocator TitleLink => new CssLocator().WithId("logo-container");       
    }
}
