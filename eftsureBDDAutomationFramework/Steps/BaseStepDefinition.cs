using System;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using TakealotBDDAutomationFramework.Helpers;
using TakealotBDDAutomationFramework.Core;
using System.Collections;

namespace TakealotBDDAutomationFramework.Steps
{
    [Binding]
    public class BaseStepDefinition
    {
        public IWebDriver driver = null;

        private readonly IObjectContainer objectContainer;

       

        public BaseStepDefinition(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeTestRun]
        private static void WarmUpIis()
        {
           
        }

        [BeforeFeature]
        private static void BeforeFeature()
        {
        }

        [Before]
        private void BeforeScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
        {  

            //setting flag to check if current feature file has @NoHeadless tag 
            bool NoHeadlessFlag = false;
            string[] featureTags = featureContext.FeatureInfo.Tags;
            foreach (var index in featureTags)//checking if feature contains the "NoHeadless" tag
            {
                if (index.Contains("NoHeadless"))
                    NoHeadlessFlag = true; //creating chrome driver with NoHeadlessFlag as true, headless mode will always be ignored                
            }

            //create driver in try catch to cater for WebDriverException BoDi.ObjectContainerException : Interface cannot be resolved, and just try again
            //creating driver            
            objectContainer.RegisterInstanceAs<IWebDriver>(
            driver = ConfigurationManager.AppSettings["Remote"] == "Yes"
                 ? WebDriverFactory.CreateRemoteDriver((DriverType)Enum.Parse(typeof(DriverType), ConfigurationManager.AppSettings["DriverType"]), NoHeadlessFlag)
                 : WebDriverFactory.CreateDriver((DriverType)Enum.Parse(typeof(DriverType), ConfigurationManager.AppSettings["DriverType"]), NoHeadlessFlag)
            );            
        }
        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {

        }

       

        [After]
        private void AfterScenario(ScenarioContext scenarioContext)
        {
           
          
            driver = objectContainer.Resolve<IWebDriver>();
            driver.Close();
            driver.Quit();
        }

        [AfterFeature]
        private static void AfterFeature()
        {
           
        }

        [AfterTestRun]
       private static void Teardown()
        {
        }

    }
}
