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

        //[OneTimeSetUp]
        //public void GenerateReportBeforeTests()
        //{
        //    ExtentReport.Instance.InitializeTest();
        //}
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

        [TearDown]
        public void AfterTest()
        {
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed)
            {
                Console.WriteLine("Test was failed");
                //ReportHelper.ExtentReport.Instance.AddScreenToLog(status);
            }
            ReportHelper.ExtentReport.Instance.FinalizeTest();
            WebDriver.Destroy();
        }
    }
}