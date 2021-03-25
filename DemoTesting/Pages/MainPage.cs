using DemoTesting.Core;
using DemoTesting.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTesting.Pages
{
    class MainPage : Core.BasePage
    {
        public void OpenStartUrl()
        {
            Navigate(Constants.Site_Url);
        }
    }
}
