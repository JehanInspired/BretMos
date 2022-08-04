using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using TakealotBDDAutomationFramework.Core;
using TakealotBDDAutomationFramework.Steps;

namespace TakealotBDDAutomationFramework.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected ElementFinder elementFinder;

        public BasePage(IWebDriver driver, string pageUrl = "")
        {
            this.driver = driver;
            try{
                PageFactory.InitElements(driver, this);//if this fails with Collection was modified; enumeration operation may not execute then just try again.
            }
            catch (InvalidOperationException)//TargetInvocationException
            {
                PageFactory.InitElements(driver, this);
            }

            new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WebDriverSettings.WaitInSeconds))
                .Until(drv =>
                    ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            if (!string.IsNullOrWhiteSpace(pageUrl))
                new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WebDriverSettings.WaitInSeconds))
                    .Until(drv => drv.Url.Contains(pageUrl));



        }
    }
}
