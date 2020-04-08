using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace CrossoverHelper
{
    public class TestReporting
    {
        public ExtentReports extent;
        public ExtentHtmlReporter htmlReporter;
        public ExtentTest test;

        public TestReporting()
        {
            htmlReporter = new ExtentHtmlReporter(@"C:\TestReport\");

            htmlReporter.Config.Theme = Theme.Dark;

            htmlReporter.Config.DocumentTitle = "Cross Over Test Report";

            htmlReporter.Config.ReportName = "Gmail_Test_Report";

            htmlReporter.Config.JS = @"$('.brand-logo').text('').append('<img src=C:\Users\ibhat\source\repos\ibhatt089\CrossoverTestAssignment\TestReporting\crosssover_logo.png>')";

            extent = new ExtentReports();

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

        public void RemoveTest()
        {
            extent.RemoveTest(test);
        }

        public void SetStepStatusPass(string stepDescription)
        {
            test.Log(Status.Pass, stepDescription);
        }

        public void SetStepStatusError(string stepDescription)
        {
            test.Log(Status.Error, stepDescription);
        }

        public void SetStepStatusFail(string stepDescription)
        {
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
            RemoveTest();
            extent.Flush();
        }
    }
}