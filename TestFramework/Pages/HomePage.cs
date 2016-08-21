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
        public CssLocator ContactForm => new CssLocator().WithName("Contact Form").WithId("contactform");
        public CssLocator MainImage => new CssLocator().WithName("Main Image").WithId("main-img");
        public CssLocator TitleLink => new CssLocator().WithName("Title Link").WithId("logo-container");       
    }
}
