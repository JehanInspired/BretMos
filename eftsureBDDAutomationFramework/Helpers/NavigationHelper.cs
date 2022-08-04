using TakealotBDDAutomationFramework.Core;
using OpenQA.Selenium;
using System.Threading;

namespace TakealotBDDAutomationFramework.Helpers
{
    public class NavigationHelper
    {
        IWebDriver driver;
        public NavigationHelper(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void NavigateToUrl(string Url)
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
