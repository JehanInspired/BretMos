using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Configuration;
using TakealotBDDAutomationFramework.Core;

namespace TakealotBDDAutomationFramework.Core
{
    class WebDriverFactory
    {
        public static IWebDriver CreateDriver(DriverType driverType, bool NoHeadlessFlag)
        {
            switch (driverType)
            {

                case DriverType.Ie:
                    return new InternetExplorerDriver();
                case DriverType.Safari:
                    return new SafariDriver();
                case DriverType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--start-maximized");
                    chromeOptions.AddArguments("--incognito");
                    chromeOptions.AddArguments("--disable-popup-blocking");
                    chromeOptions.AddArguments("--disable-notifications");

                    if (!NoHeadlessFlag)//if the feature contains the tag "import", then ignore headless arguments.
                    {
                      chromeOptions.AddArguments("--headless"); //using headless mode causes import scenarios to fail (fileupload fails)
                      chromeOptions.AddArguments("--disable-gpu");//disable gpu best used when headless mode is used
                    }
                    chromeOptions.AddArguments("--window-size=1920,1080");//used to improve screenshot image/debugging screenshots
                    chromeOptions.AddExcludedArgument("enable-automation");
                    
                    //chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                    return new ChromeDriver(chromeOptions);
                case DriverType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("--start-maximized");
                    firefoxOptions.AddArguments("--incognito");
                    firefoxOptions.AddArguments("--disable-popup-blocking");
                    firefoxOptions.AddArguments("--disable-notifications");
                    if (!NoHeadlessFlag)//if the feature contains the tag "import", then ignore headless arguments.
                    {
                        firefoxOptions.AddArguments("--headless"); //using headless mode causes import scenarios to fail (fileupload fails)
                        firefoxOptions.AddArguments("--disable-gpu");//disable gpu best used when headless mode is used
                    }
                    return new FirefoxDriver(firefoxOptions);

                default:
                    //maybe throw error saying invalid driver type?
                    return new ChromeDriver();
            }
        }

        public static IWebDriver CreateRemoteDriver(DriverType driverType, bool NoHeadlessFlag)
        {
            switch (driverType)
            {
                case DriverType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("--start-maximized");
                    firefoxOptions.AddArguments("--incognito");
                    firefoxOptions.AddArguments("--disable-popup-blocking");
                    firefoxOptions.AddArguments("--disable-notifications");
                    if (!NoHeadlessFlag)//if the feature contains the tag "import", then ignore headless arguments.
                    {
                        firefoxOptions.AddArguments("--headless"); //using headless mode causes import scenarios to fail (fileupload fails)
                        firefoxOptions.AddArguments("--disable-gpu");//disable gpu best used when headless mode is used
                    }
                    return new RemoteWebDriver(firefoxOptions);
                //case DriverType.Ie:
                //    var ieOptions = new InternetExplorerOptions();
                //    ieOptions.AddAdditionalCapability("ignoreProtectedModeSettings", true);
                //    ieOptions.AddAdditionalCapability("EnsureCleanSession", true);
                //    return new InternetExplorerDriver();
                //case DriverType.SafariCbt:
                //    var safariOptionsCbt = new SafariOptions();
                //    safariOptionsCbt.AddAdditionalCapability("name", "SpecFlow-CBT-Safari");
                //    safariOptionsCbt.AddAdditionalCapability("build", "1.35");
                //    safariOptionsCbt.AddAdditionalCapability("browserName", "Safari");
                //    safariOptionsCbt.AddAdditionalCapability("version", "11");
                //    safariOptionsCbt.AddAdditionalCapability("platform", "Mac OSX 10.13");
                //    safariOptionsCbt.AddAdditionalCapability("screenResolution", "1366x768");
                //    safariOptionsCbt.AddAdditionalCapability("record_video", "true");
                //    safariOptionsCbt.AddAdditionalCapability("record_network", "true");
                //    safariOptionsCbt.AddAdditionalCapability("username", ConfigurationManager.AppSettings["RemoteUserNameCBT"]);
                //    safariOptionsCbt.AddAdditionalCapability("password", ConfigurationManager.AppSettings["RemoteAuthKeyCBT"]);
                //    return new RemoteWebDriver(new Uri("http://hub.crossbrowsertesting.com:80/wd/hub"), safariOptionsCbt.ToCapabilities(), TimeSpan.FromSeconds(180));
                //case DriverType.SafariBs:
                //    var safariOptionsBs = new SafariOptions();
                //    safariOptionsBs.AddAdditionalCapability("name", "SpecFlow-BS-Safari");
                //    safariOptionsBs.AddAdditionalCapability("browserstack.local", "true");
                //    safariOptionsBs.AddAdditionalCapability("browserstack.debug", "true");
                //    safariOptionsBs.AddAdditionalCapability("build", "1.35");
                //    safariOptionsBs.AddAdditionalCapability("browser", "Safari");
                //    safariOptionsBs.AddAdditionalCapability("browser_version", "7.1");
                //    safariOptionsBs.AddAdditionalCapability("os", "OS X");
                //    safariOptionsBs.AddAdditionalCapability("os_version", "Mavericks");
                //    safariOptionsBs.AddAdditionalCapability("resolution", "1024x768");
                //    safariOptionsBs.AddAdditionalCapability("browserstack.user", ConfigurationManager.AppSettings["RemoteUserNameBS"]);
                //    safariOptionsBs.AddAdditionalCapability("browserstack.key", ConfigurationManager.AppSettings["RemoteAuthKeyBS"]);
                //    return new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub"), safariOptionsBs.ToCapabilities(), TimeSpan.FromSeconds(180));
                case DriverType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--start-maximized");
                    chromeOptions.AddArguments("--incognito");
                    chromeOptions.AddArguments("--disable-popup-blocking");
                    chromeOptions.AddArguments("--disable-notifications");
                    chromeOptions.AddArguments("--disable-infobars");
                    chromeOptions.AddArguments("--window-size=1920,1080");
                    if (!NoHeadlessFlag)//if the feature contains the tag "import", then ignore headless arguments.
                    {
                        chromeOptions.AddArguments("--headless"); //using headless mode causes import scenarios to fail (fileupload fails)
                        chromeOptions.AddArguments("--disable-gpu");//disable gpu best used when headless mode is used
                    }
                    return new RemoteWebDriver(new Uri(ConfigurationManager.AppSettings["GridURL"]),chromeOptions);
                default:
                    return new ChromeDriver();
            }
        }

    }
}
