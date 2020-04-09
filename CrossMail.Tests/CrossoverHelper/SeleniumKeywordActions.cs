using System;
using OpenQA.Selenium;
using Xunit;

namespace CrossMail.Tests.CrossoverHelper
{
    /// <summary>
    /// Generic Use - Selenium Actions Wrapper Class
    /// </summary>
    public static class SeleniumKeywordActions
    {
        /// <summary>
        /// Exception Message String Variable
        /// </summary>
        public static bool FailedErrorStep = false;

        /// <summary>
        /// Launch - GoToUrl Wrapper Method
        /// </summary>
        /// <param name="driver">IWebDriver Object</param>
        /// <param name="report">TestReporting Object</param>
        /// <param name="url">Web URL String</param>
        public static void LaunchUrl(this IWebDriver driver, TestReporting report, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                report.SetStepStatusPass($"URL [{url}] launched successfully.");
            }
            catch (Exception e)
            {
                report.SetStepStatusError(e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Function to Verify Text Assertion
        /// </summary>
        /// <param name="report">TestReporting Object</param>
        /// <param name="expectedText">Expected Text String</param>
        /// <param name="actualText">Actual Text String</param>
        /// <param name="stepDescription">Test Step Description</param>
        public static void VerifyText(TestReporting report, string expectedText, string actualText, string stepDescription)
        {
            try
            {
                Assert.True(expectedText == actualText);
                report.SetStepStatusPass("Assertion Passed for Condition : " + stepDescription
                                            + "<br>Expected Text: " + expectedText + "<br>"
                                            + "Actual Text:" + actualText);
            }
            catch(Exception e)
            {
                report.SetStepStatusFail("Assertion Failed for Condition : " + stepDescription
                                            + "<br>Expected Text: " + expectedText + "<br>"
                                            + "Actual Text:" + actualText);
            }
        }

        /// <summary>
        /// Clear Action Wrapper Method
        /// </summary>
        /// <param name="element">IWebElement Object</param>
        /// <param name="report">TestReporting Object</param>
        /// <param name="elementName">Web Element Name</param>
        public static void Clear(this IWebElement element, TestReporting report, string elementName)
        {
            element.Clear();

            if (element.Text.Equals(string.Empty))
            {
                report.SetStepStatusPass($"Cleared element [{elementName}] content.");
            }
            else
            {
                report.SetStepStatusError($"Element [{elementName}] content is not cleared. Element value is [{element.Text}]");
                throw new Exception($"Element [{elementName}] content is not cleared.");
            }
        }

        /// <summary>
        /// Click Action Wrapper Method
        /// </summary>
        /// <param name="element">IWebElement Object</param>
        /// <param name="report">TestReporting Object</param>
        /// <param name="elementName">Web Element Name</param>
        public static void Click(this IWebElement element, TestReporting report, string elementName)
        {
            if (element.Displayed && element.Enabled)
            {
                element.Click();
                report.SetStepStatusPass($"Clicked on the element [{elementName}].");
            }
            else
            {
                report.SetStepStatusError($"Element [{elementName}] is not displayed");
                throw new Exception($"Element [{elementName}] is not displayed");
            }
        }

        /// <summary>
        /// SendKeys Action Wrapper Method
        /// </summary>
        /// <param name="element">IWebElement Object</param>
        /// <param name="report">TestReporting Object</param>
        /// <param name="value">Ttext Input Value String</param>
        /// <param name="elementName">Web Element Name</param>
        public static void SendKeys(this IWebElement element, TestReporting report, string value, string elementName)
        {
            try
            {
                element.SendKeys(value);
                report.SetStepStatusPass($"SendKeys value [{value}] to  element [{elementName}].");
            }
            catch (Exception e)
            {
                report.SetStepStatusError(e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Function to capture Screenshot.
        /// </summary>
        /// <param name="driver">IWebDriver Object</param>
        /// <returns>Returns Base 64 Screenshot String</returns>
        public static string ScreenCaptureAsBase64String(this IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }
    }
}