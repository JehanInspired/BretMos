using TakealotBDDAutomationFramework.Core;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakealotBDDAutomationFramework.Pages
{
     class TakealotPage: BasePage
    {
        public TakealotPage(IWebDriver driver) : base(driver, "")
        {
            this.driver = driver;
            elementFinder = new ElementFinder(driver);
        }
        //set all elements
        #region elements
        //header
        [FindsBy(How = How.ClassName, Using = "search-field")]
        private IWebElement inputSearchBox { get; set; } 
        [FindsBy(How = How.ClassName, Using = "search-icon")]
        private IWebElement btnSearch { get; set; }  
        [FindsBy(How = How.XPath, Using = "//button[@data-ref='add-to-cart-button']")]
        private IWebElement btnAddItem { get; set; } 
        [FindsBy(How = How.ClassName, Using = "mini-cart-button")]
        private IWebElement btnShoppingCart { get; set; }
        [FindsBy(How = How.XPath, Using = "//p[@class='cart-summary-module_cart-summary-item-count_3jkNC']//span[1]")]
        private IWebElement cartSummary { get; set; }
        #endregion

        //set all actions on elements
        #region actions
        public void InputSearchBox(string text)
        {
            elementFinder.WaitForElementToBeClickable(inputSearchBox);
            elementFinder.SetTextboxText(inputSearchBox, text);
        }   
        public void SelectSearch()
        {
            elementFinder.WaitForElementToBeClickable(btnSearch);
            elementFinder.ClickOnElement(btnSearch);
        }

        public void SelectAddToCart()
        {
            elementFinder.WaitForElementToBeClickable(btnSearch);
            elementFinder.ClickOnElement(btnAddItem);

        }
        public void selectCartIcon()
        {
            elementFinder.WaitForElementToBeClickable(btnShoppingCart);
            elementFinder.ClickOnElement(btnShoppingCart);
        } 
        public string getQTYText()
        {
            return elementFinder.GetText(cartSummary); 
        }
       
        
        #endregion
    }
}

