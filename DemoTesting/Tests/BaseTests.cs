using NUnit.Framework;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using System;
using DemoTesting.Core;
using DemoTesting.Pages;
using NUnit.Framework.Interfaces;

namespace DemoTesting
{
    [TestFixture]
    [AllureNUnit]
    [AllureDisplayIgnored]
    public class Tests
    {
        private MainPage mainPage;

        [SetUp]
        public void BeforeTest()
        {
            mainPage = new MainPage();            
        }

        [Test]
        public void OpenHomePage()
        {
            mainPage.OpenStartUrl();
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed)
            {
                Console.WriteLine("Test was failed");
                WebDriver.MakeScreenShot();
            }
            WebDriver.Destroy();
        }
    }
}