﻿using System;
using AutoItX3Lib;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CrossoverHelper;
using Xunit;

namespace CrossMail.Tests
{
    /// <summary>
    /// Gmail Test Class
    /// </summary>
    public class GmailTests : IDisposable
    {
        readonly IWebDriver _browserDriver;
        readonly IConfiguration _config;
        protected TestReporting testReporting;
        string testStatus = "fail";

        /// <summary>
        /// Constructor Method for Pre-Test Initialize Steps
        /// </summary>
        public GmailTests()
        {
            _browserDriver = new ChromeDriver("./");

            _config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _browserDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Convert.ToInt32(_config["_pageLoadTimeout"]));
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToInt32(_config["_implicitWaitTimeSpan"]));
        }

        /// <summary>
        /// Dispose Method for Test Class
        /// </summary>
        public void Dispose()
        {
            switch (testStatus)
            {
                case "pass":
                    testReporting.SetTestStatusPass();
                    break;

                case "fail":
                    testReporting.SetTestStatusFail("Test Case Failed.");
                    break;

            }

            try
            {
                _browserDriver.Quit();
                testReporting.Close();
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Test Method - Fact - for Sending Email via Gmail Web Portal.
        /// </summary>
        [Fact]
        public void ShouldSendEmail()
        {
            testReporting = new TestReporting();
            
            testReporting.CreateTest("ShouldSendEmail");

            testReporting.SetStepStatusPass("Chrome Driver started.");

            testReporting.SetStepStatusPass("Json Config File Loaded Successfully.");

            // Navigate and launch the Gmail URL
            _browserDriver.LaunchUrl(testReporting, "https://mail.google.com/");
            _browserDriver.Manage().Window.Maximize();
            testReporting.SetStepStatusPass("BRowser Maximized.");

            // Enter the Username
            IWebElement userElement = _browserDriver.FindElement(By.Id(_config["_objectUserNameTextBox"]));
            userElement.SendKeys(testReporting, _config["username"], "User Name");

            // Click on the Next Button
            IWebElement nextButton = _browserDriver.FindElement(By.Id(_config["_objectUserNameNextButton"]));
            nextButton.Click(testReporting, "Username Next Button");

            // Enter the Password
            IWebElement passwordElement = _browserDriver.FindElement(By.Name(_config["_objectPasswordTextBox"]));
            if (passwordElement.Displayed && passwordElement.Enabled)
                passwordElement.Click(testReporting, "Password Text Box");

            passwordElement.SendKeys(testReporting, _config["password"], "Password Text Box");

            // Click on the Next Button
            nextButton = _browserDriver.FindElement(By.Id(_config["_objectUserPasswordNextButton"]));
            nextButton.Click(testReporting, "Password Next Button");

            // Click on the Compose Button
            IWebElement composeElement = _browserDriver.FindElement(By.XPath(_config["_objectComposeButton"]));
            composeElement.Click(testReporting, "Compose Email Button");

            // Enter the Recipient's Email Address
            IWebElement toElement = _browserDriver.FindElement(By.Name(_config["_objectToTextBox"]));
            toElement.Clear(testReporting, "Recipient's To Email Address Text Box");
            toElement.SendKeys(testReporting, $"{_config["username"]}@gmail.com", "Recipient's To Email Address Text Box");

            // Enter the Subject Line with Unique Date Time Stamp
            IWebElement subjectTextElement = _browserDriver.FindElement(By.Name(_config["objectSubjectTextBox"]));
            subjectTextElement.Clear(testReporting, "Subject Line Text Box");
            string subjectLineText = "Autogenerated Crossover Test User Email generated at " + DateTime.UtcNow.ToString();
            subjectTextElement.SendKeys(testReporting, subjectLineText, "Subject Line Text Box");

            // Enter the mail body text.
            string mailBodyText = "This is an Auto Generated Test Email.";
            IWebElement mailBodyElement = _browserDriver.FindElement(By.XPath(_config["objectMailBodyTextBox"]));
            mailBodyElement.Clear(testReporting, "Mail Body Text Box");
            mailBodyElement.SendKeys(testReporting, mailBodyText, "Mail Body Text Box");

            // Click on the More Options Button
            IWebElement moreOptionsElement = _browserDriver.FindElement(By.XPath(_config["objectMoreOptionsButton"]));
            moreOptionsElement.Click(testReporting, "More Options Button");

            // Click on the Labels Menu Item
            IWebElement labelMenuItemElement = _browserDriver.FindElement(By.XPath(_config["objectLabelMenuItem"]));
            labelMenuItemElement.Click(testReporting, "Label Menu Options Button");

            // Click and Add 'Social' Label to the Email
            IWebElement socialLabelElement = _browserDriver.FindElement(By.XPath(_config["objectSocialLabelMenuItem"]));
            socialLabelElement.Click(testReporting, "Social Label");

            // Add Attachment to the Mail
            _browserDriver.FindElement(By.XPath(_config["objectAttachFilesButton"])).Click();

            System.Threading.Thread.Sleep(5000);
            FileUpload(_config["_filePath"]);

            // Click on the Send Mail Button
            IWebElement sendButtonElement = _browserDriver.FindElement(By.XPath(_config["_objectSendMailButton"]));
            sendButtonElement.Click(testReporting, "Send Mail Button");
            testStatus = "pass";
            testReporting.Close();
        }

        /// <summary>
        /// Method for Uploading File from Windows Dialogue Box
        /// </summary>
        /// <param name="filePath">File Path String</param>
        private void FileUpload(string filePath)
        {
            AutoItX3 autoItX = new AutoItX3();
            autoItX.WinActivate("Open");
            autoItX.Send(filePath);
            System.Threading.Thread.Sleep(2000);
            autoItX.Send("{ENTER}");
            testReporting.SetStepStatusPass("File successfully attached with the mail.");
        }
    }
}