using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace CrossMail.Tests.CrossoverHelper
{
    public class TestReporting
    {
        public AventStack.ExtentReports.ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        public ExtentTest test;

        public TestReporting()
        {
            htmlReporter = new ExtentHtmlReporter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            htmlReporter.Config.Theme = Theme.Dark;

            htmlReporter.Config.DocumentTitle = "Cross Over Test Report";

            htmlReporter.Config.ReportName = "Gmail_Test_Report";

            htmlReporter.Config.JS = @"$('.brand-logo').text('').append('<img src=C:\Users\ibhat\source\repos\ibhatt089\CrossoverTestAssignment\crossover-white.png>')";

            extent = new AventStack.ExtentReports.ExtentReports();

            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Application Under Test", "Gmail");

            extent.AddSystemInfo("Test Environment", "Remote QA");

            extent.AddSystemInfo("Test Machine Name", Environment.MachineName);

            extent.AddSystemInfo("Operating System", Environment.OSVersion.VersionString);
        }

        public void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public void SetStepStatusPass(string stepDescription)
        {
            test.Log(Status.Pass, stepDescription);
        }

        public void SetStepStatusError(string stepDescription)
        {
            SeleniumKeywordActions.FailedErrorStep = true;
            test.Log(Status.Error, stepDescription);
        }

        public void SetStepStatusFail(string stepDescription)
        {
            SeleniumKeywordActions.FailedErrorStep = true;
            test.Log(Status.Fail, stepDescription);
        }

        public void SetStepStatusWarning(string stepDescription)
        {
            test.Log(Status.Warning, stepDescription);
        }

        public void SetTestStatusPass()
        {
            test.Pass("Test Executed Sucessfully!");
        }

        public void SetTestStatusFail(string message = null)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";

            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }
            test.Fail(printMessage);
        }

        /// <summary>
        /// Function to capture Screenshot of Failed Screen.
        /// </summary>
        /// <param name="base64ScreenCapture"></param>
        public void AddTestFailureScreenshot(string base64ScreenCapture)
        {
            test.AddScreenCaptureFromBase64String(base64ScreenCapture, "Error Screenshot:");
        }

        public void SetTestStatusSkipped()
        {
            test.Skip("Test skipped!");
        }

        public void Close()
        {
            extent.Flush();
        }
    }
}