using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CrossoverHelper
{
    public static class SeleniumKeywordActions
    {
        public static string ExceptionMessage = string.Empty;

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

        public static void Clear(this IWebElement element, TestReporting report, string elementName)
        {
            element.Clear();

            if (element.Text.Equals(string.Empty))
            {
                report.SetStepStatusPass($"Cleared element [{elementName}] content.");
            }
            else
            {
                report.SetStepStatusWarning($"Element [{elementName}] content is not cleared. Element value is [{element.Text}]");
            }
        }

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

        public static void SendKeys(this IWebElement element, TestReporting report, string value, string elementName)
        {
            element.SendKeys(value);
            report.SetStepStatusPass($"SendKeys value [{value}] to  element [{elementName}].");
        }

        public static string ScreenCaptureAsBase64String(this IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }
    }
}