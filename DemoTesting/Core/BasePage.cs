using System;
using System.Collections.Generic;
using System.Text;
using DemoTesting.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DemoTesting.Core
{
    class BasePage
    {
        protected void Navigate(String url)
        {
            WebDriver.CurrentDriver.Navigate().GoToUrl(url);
        }
        public IWebElement GetWebElement(By locator)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver.CurrentDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return WebDriver.CurrentDriver.FindElement(locator);
        }
        public IWebElement WaitForClicableElement(By locator)
        {
            WebDriverWait wait = new WebDriverWait(WebDriver.CurrentDriver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            return WebDriver.CurrentDriver.FindElement(locator);
        }
    }
}