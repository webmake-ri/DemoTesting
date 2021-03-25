using System;
using System.Collections.Generic;
using System.Text;
using DemoTesting.Utils;

namespace DemoTesting.Core
{
    class BasePage
    {
        protected void Navigate(String url)
        {
            WebDriver.CurrentDriver.Navigate().GoToUrl(url);
        }
    }
}
