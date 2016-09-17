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
        public CssLocator GithubHeaderLink => new CssLocator().WithName("GitHub Header Link").WithId("github-header-link");
        public CssLocator TwitterHeaderLink => new CssLocator().WithName("GitHub Header Link").WithId("twitter-header-link");
        public CssLocator FacebookHeaderLink => new CssLocator().WithName("GitHub Header Link").WithId("facebook-header-link");
        public CssLocator GooglePlusHeaderLink => new CssLocator().WithName("GitHub Header Link").WithId("googleplus-header-link");
        public CssLocator LinkedInHeaderLink => new CssLocator().WithName("GitHub Header Link").WithId("linkedin-header-link");
        public CssLocator DashboardHeaderLink => new CssLocator().WithName("GitHub Header Link").WithId("dashboard-header-link");
    }
}
