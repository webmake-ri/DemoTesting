using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DemoTesting.Core
{
    class WebDriver
    {
        public static IWebDriver driver;
        private static readonly ThreadLocal<WebDriver> threadLocal =
                new ThreadLocal<WebDriver>(() => new WebDriver());
        private static readonly object ThreadLock = new object();
        public static WebDriver Instance { get { return threadLocal.Value; } }

        public static IWebDriver CurrentDriver
        {
            get => WebDriver.GetInstance();
        }

        public static EventFiringWebDriver GetInstance()
        {
            if (driver == null) 
            {
                lock (ThreadLock)
                {
                    if (driver == null)
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--start-maximized");
                        driver = new ChromeDriver(options);
                    }
                }
            }
            EventFiringWebDriver eventsDriver = new EventFiringWebDriver(driver);
            eventsDriver.Navigated += EventsDriver_Navigated;
            eventsDriver.ScriptExecuting += EventsDriver_Script;
            eventsDriver.ElementClicking += ElementClicking;
            return eventsDriver;
        }
        private static void ElementClicking(object sender, WebElementEventArgs e)
        {
            ScrollToElement(e.Element);
        }
        public static void ScrollToElement(IWebElement e)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(false);", e);
        }
        private static void EventsDriver_Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private static void EventsDriver_Script(object sender, WebDriverScriptEventArgs e)
        {
            _ = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public static void Destroy()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
                driver = null;
            }
        }
    }
}
