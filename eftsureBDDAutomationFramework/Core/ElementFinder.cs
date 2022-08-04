using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TakealotBDDAutomationFramework.Steps;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using System.Reflection;

namespace TakealotBDDAutomationFramework.Core
{
    public class ElementFinder
    {
        IWebDriver driver;
        public ElementFinder(IWebDriver driver)
        {
            this.driver = driver;
        }

        #region Core Methods
       
        public string GetAttribute(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }       
        public string GetText(IWebElement element)
        {
            WaitForElementToBeClickable(element);//nb important! when getting text, the element must be visible 
            return element.Text;
        }
        public IWebElement FindElement(By findBy, int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv => drv.FindElement(findBy));
        }
        public bool IsElementVisible(IWebElement element)
        {
            try
            {
                return element != null && element.Displayed && element.Enabled && element.Size.Height > 0;
            }
            catch (TargetInvocationException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        #endregion

        # region Driver Actions
        public void ScrollToBottom()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }
        public void ScrollToTop()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0)");
        }
        public IWebElement ClickOnElement(IWebElement element)
        {
            WaitForElementToBeClickable(element);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + element.Location.X + "," + element.Location.Y + ")");            
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            return element;
        }
        /// <summary>
        /// Use this if the element to be clicked doesnt work with ClickOnElement.
        /// Normally elements that dont have any dimentions within the actual element. eg input tags on a div
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IWebElement ClickOnElementAction(IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Click();
            actions.Build().Perform();
            return element;
        }
        public void SetTextboxText(IWebElement element, string input)
        {
            //wait for textbox and scroll to it
            WaitForDomVisible(element);
            //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(" + element.Location.X + "," + element.Location.Y + ")");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView();",element);
            //set and verify textbox               
            var textVerified = false;
            var timeout = 0;
            while (!textVerified && timeout < 30)
            {
                element.Clear();
                element.SendKeys(input);
                if (GetAttribute(element, "value") != input)
                {
                    Thread.Sleep(200);
                    timeout++;
                }
                else
                    textVerified = true;
            }
        }
        public void ClearSelectBox(IWebElement el)
        {
            var select = new SelectElement(el);
            select.DeselectAll();
        }

        #endregion

        # region Driver Waits
        public void WaitForDomVisible(IWebElement element, int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
            for (int i = 0; i < timeoutInSeconds; i++)
            {
                if (IsElementVisible(element))
                    return;
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            throw new Exception(string.Format("Waited too long for element to become visible : " + timeoutInSeconds + " seconds -> "));
        }
        public void WaitForDomInvisible(IWebElement element, int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(x =>
            {
                return !IsElementVisible(element);
            });
        }
        public void WaitForLoadingIndicator(int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
            //finding all blockUI elements and waiting for all of them to be invisible
            ReadOnlyCollection<IWebElement> loadingIndicators = driver.FindElements(By.CssSelector(".blockUI"));
            foreach (IWebElement loadingIndicator in loadingIndicators)
            {
                try
                {
                    WaitForDomInvisible(loadingIndicator, timeoutInSeconds);
                }
                catch (StaleElementReferenceException)
                {
                    //if element is stale, page is not loading, do nothing
                }
            }                     
        }
        public void WaitForAjax(int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
           // WaitForLoadingIndicator(timeoutInSeconds);
            bool success = false;
            while (timeoutInSeconds > 0)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                {
                    success = true;
                    break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                timeoutInSeconds -= 1;
            }
            if (!success)
                throw new Exception("Waited too long for AJAX call > " + Constants.WebDriverSettings.WaitInSeconds + " seconds");
        }
        public void WaitForTextToBePresent(IWebElement textElement)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.WebDriverSettings.WaitInSeconds));
                wait.Until(d => textElement.Text.Length > 0);
            }
            catch (Exception)
            {
                Console.WriteLine($"Text is not displayed.\n{textElement.GetAttribute("outerHTML")}");
                throw;
            }
        }

        //SeleniumExtraWaits
        public IWebElement WaitForElementToBeClickable(IWebElement element, int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
        public bool WaitForElementToContainText(IWebElement element,string text, int timeoutInSeconds = Constants.WebDriverSettings.WaitInSeconds)
        {
            try{
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, text));
            }catch (WebDriverTimeoutException){
                return false;
            }
        }
        #endregion


    }
}
