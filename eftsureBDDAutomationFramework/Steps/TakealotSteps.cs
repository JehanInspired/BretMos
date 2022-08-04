using TakealotBDDAutomationFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace TakealotBDDAutomationFramework.Steps
{
    [Binding]
    class TakealotSteps
    {
        public TakealotSteps(IWebDriver driver)
        {
            this.driver = driver;

            navigationSteps = new NavigationSteps(driver);
            takealotPage = new TakealotPage(driver);
        }
        //set driver/ helpers
        IWebDriver driver;
        NavigationSteps navigationSteps;
        TakealotPage takealotPage;
    

        [Given(@"I have searched for a product")]
        public void GivenIHaveSearchedForAProduct()
        {
           
            takealotPage.InputSearchBox("Marvel The Avengers Iron Man Charm");
            takealotPage.SelectSearch();
        }

        [When(@"I select add to cart")]
        public void WhenISelectAddToCart()
        {
            takealotPage.SelectAddToCart();
            takealotPage.selectCartIcon();
        }

        [Then(@"The product will be added to the cart")]
        public void ThenTheProductWillBeAddedToTheCart()
        {
            var actual = takealotPage.getQTYText();
            Assert.AreEqual("(1 item)", actual);
        }

    }
}
