using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CrossMail.Tests.CrossoverHelper
{
    /// <summary>
    /// Test Project Specific Validation Class
    /// </summary>
    public static class ProjectSpecificValidationHelper
    {
        /// <summary>
        /// Method to Validate the Sent Email's Label As 'Social'
        /// </summary>
        /// <param name="driver">IWebDriver Object</param>
        /// <param name="report">TestReporting Class Object</param>
        public static void ValidateSocialLabelElement(this IWebDriver driver, TestReporting report)
        {
            try
            {
                Assert.True(driver.FindElement(By.XPath("//*[@name='^smartlabel_social']")).Displayed);
                report.SetStepStatusPass("Email's label Validated as : 'Social'");
            }
            catch (Exception e)
            {
                report.SetStepStatusFail("Email's Label Validation Failed.");
                report.AddTestFailureScreenshot(SeleniumKeywordActions.ScreenCaptureAsBase64String(driver));
            }
        }

        /// <summary>
        /// Method to Validate the Sent Email's Subject Line Text.
        /// </summary>
        /// <param name="driver">IWebDriver Object</param>
        /// <param name="report">TestReporting Class Object</param>
        /// <param name="subjectLineText">Subject Line Text String</param>
        public static void ValidateSubjectLineTextElement(this IWebDriver driver, TestReporting report, string subjectLineText)
        {
            try
            {
                Assert.True(driver.FindElement(By.XPath("//h2[contains(text(),'" + subjectLineText + "')]")).Displayed);
                report.SetStepStatusPass("Email's Subject Line Element Validated as: " + subjectLineText);
            }
            catch (Exception e)
            {
                report.SetStepStatusFail("Email's Subject Line Element Validation Failed.");
                report.AddTestFailureScreenshot(SeleniumKeywordActions.ScreenCaptureAsBase64String(driver));
            }
        }

        /// <summary>
        /// Method to Validate the Sent Email's Mail Body Text.
        /// </summary>
        /// <param name="driver">IWebDriver Object</param>
        /// <param name="report">TestReporting Class Object</param>
        /// <param name="mailBodyText">Mail Body Text String</param>
        public static void ValidateMailBodyTextElement(this IWebDriver driver, TestReporting report, string mailBodyText)
        {
            try
            {
                Assert.True(driver.FindElement(By.XPath("//div[text()='" + mailBodyText + "']")).Displayed);
                report.SetStepStatusPass("Email's Mail Body Element Validated as: " + mailBodyText);
            }
            catch (Exception e)
            {
                report.SetStepStatusFail("Email's Mail Body Element Validation Failed.");
                report.AddTestFailureScreenshot(SeleniumKeywordActions.ScreenCaptureAsBase64String(driver));
            }
        }

        /// <summary>
        /// Method to Validate the Sent Email's Attachment Name Text.
        /// </summary>
        /// <param name="driver">IWebDriver Object</param>
        /// <param name="report">TestReporting Class Object</param>
        /// <param name="attachmentFileName">Attachment File Name String</param>
        public static void ValidateAttachmentNameTextElement(this IWebDriver driver, TestReporting report, string attachmentFileName)
        {
            try
            {
                Assert.True(driver.FindElement(By.XPath("//*[text()='" + attachmentFileName + "']")).Displayed);
                report.SetStepStatusPass("Email's Attachment Name Element Validated as: " + attachmentFileName);
            }
            catch (Exception e)
            {
                report.SetStepStatusFail("Email's Attachment Name Element Validation Failed.");
                report.AddTestFailureScreenshot(SeleniumKeywordActions.ScreenCaptureAsBase64String(driver));
            }
        }
    }
}