using NUnit.Framework;
using System;
using DemoTesting.Core;
using DemoTesting.Pages;
using NUnit.Framework.Interfaces;
using DemoTesting.ReportHelper;
using AventStack.ExtentReports;

namespace DemoTesting
{
    [TestFixture]
    public class Tests
    {
        private MainPage mainPage;

        [SetUp]
        public void BeforeTest()
        {
            ExtentReport.Instance.InitializeTest();
            mainPage = new MainPage();            
        }

        [Test]
        public void OpenHomePage()
        {
            mainPage.OpenStartUrl();
            Assert.AreEqual(WebDriver.driver.Url, Utils.Constants.Site_Url);
        }
        [Test]
        public void TestFailed()
        {
            mainPage.OpenStartUrl();
            Assert.AreEqual(WebDriver.driver.Url, "https://google.com");
        }
        [TearDown]
        public void AfterTest()
        {
            Status status = (Status)TestContext.CurrentContext.Result.Outcome.Status;
            if (status == Status.Fail)
            {
                Console.WriteLine("Test was failed");
            }
            ReportHelper.ExtentReport.Instance.FinalizeTest();
            WebDriver.Destroy();
        }
    }
}