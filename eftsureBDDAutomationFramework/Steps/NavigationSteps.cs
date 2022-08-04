using TakealotBDDAutomationFramework.Core;
using TakealotBDDAutomationFramework.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace TakealotBDDAutomationFramework.Steps
{
    [Binding]
    public class NavigationSteps
    {
        public NavigationSteps(IWebDriver driver)
        {
            this.driver = driver;
            navigationHelper = new NavigationHelper(driver);
        }

        IWebDriver driver;
        NavigationHelper navigationHelper;

        [Given(@"I have navigated to the takealot landingpage")]
        public void GivenIHaveNavigatedToTheTakealotLandingpage()
        {
            navigationHelper.NavigateToUrl("https://www.takealot.com/");
        }

    }
}