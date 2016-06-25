using System.Configuration;

namespace TestFramework.Pages
{
    public class ResumePage : IPage
    {
        public string Name => "Resume Page";
        public string Url => ConfigurationManager.AppSettings["ResumePageUrl"];
    }
}
