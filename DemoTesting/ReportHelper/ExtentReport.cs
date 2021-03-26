using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoTesting.ReportHelper
{
    class ExtentReport
    {
		private ExtentReports extent;
		private ExtentTest test;
		private static ExtentReport instance;
		private static string pathToLogFolder;
		private int screenshotCount;
		/// <summary>
		/// Implement the singleton for class ReportHelper
		/// </summary>
		public static ExtentReport Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ExtentReport();
				}
				return instance;
			}
		}

		private ExtentReport()
		{
			extent = new ExtentReports();
			pathToLogFolder = $"ExtentReports\\TestsResult{DateTime.Now.ToString(" hh-mm (dd.MM.yyyy)")}\\";
			var reporter = new ExtentHtmlReporter(pathToLogFolder);
			extent.AttachReporter(reporter);
		}

		/// <summary>
		/// Initialize Test
		/// </summary>
		public ExtentTest InitializeTest()
		{
			TestContext context = TestContext.CurrentContext;
			if (context.Test.Properties["Description"].Any())
			{
				string description = context.Test.Properties["Description"].First().ToString();
				test = extent.CreateTest(context.Test.Name, description);
			}
			else
			{
				test = extent.CreateTest(context.Test.Name);
			}
			foreach (var category in context.Test.Properties["Category"])
			{
				test.AssignCategory(category.ToString());
			}
			return test;
		}

		public void FinalizeTest(bool isApiTest = false)
		{
			var status = TestContext.CurrentContext.Result.Outcome.Status;

			Status logstatus;

			switch (status)
			{
				case TestStatus.Failed:
					logstatus = Status.Fail;
					/*if (!isApiTest)
					{
						AddScreenToLog(logstatus);
					}*/
					test.Log(logstatus, TestContext.CurrentContext.Result.Message);
					test.Log(logstatus, TestContext.CurrentContext.Result.StackTrace);
					break;
				case TestStatus.Inconclusive:
					logstatus = Status.Warning;
					break;
				case TestStatus.Skipped:
					logstatus = Status.Skip;
					break;
				default:
					logstatus = Status.Pass;
					break;
			}
			test.Log(logstatus, "Test ended with " + logstatus);
			extent.Flush();
		}

		public void AddScreenToLog(TestStatus status)
		{
			var mediaModel = MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build();
			test.Fail("details", mediaModel);

			// or:
			test.Fail("details",
				MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
		}

		public void Info(string text)
		{
			test.Log(Status.Info, text);
		}

		public void Warn(string text)
		{
			test.Log(Status.Warning, text);
		}

		public void CleanUpReporting()
		{
			extent.Flush();
		}
	}
}
