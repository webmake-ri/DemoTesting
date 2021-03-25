using Allure.Commons;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTesting.Core
{
    class WebDriver
    {
        public static IWebDriver driver;
        public static IWebDriver CurrentDriver
        {
            get { return WebDriver.GetInstance(); }
        }

        public static EventFiringWebDriver GetInstance()
        {
            if (driver == null)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                driver = new ChromeDriver(options);
            }

            EventFiringWebDriver eventsDriver = new EventFiringWebDriver(driver);
            return eventsDriver;
        }
        public static void MakeScreenShot()
        {
            AllureLifecycle.Instance.AddAttachment($"Screenshot [{DateTime.Now:HH:mm:ss}]",
                 "image/png",
                 driver.TakeScreenshot().AsByteArray);
        }
        public static void Destroy()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
