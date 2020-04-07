using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using Xunit;

namespace CrossMail.Tests
{
    public class GMailTests : IDisposable
    {
        readonly IWebDriver _browserDriver;
        readonly IConfiguration _config;

        public GMailTests()
        {
            _browserDriver = new ChromeDriver("./");
            _config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _browserDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Convert.ToInt32(_config["_pageLoadTimeout"]));
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToInt32(_config["_implicitWaitTimeSpan"]));
        }

        public void Dispose()
        {
            _browserDriver.Quit();
        }

        [Fact]
        public void Should_Send_Email()
        {
            // Navigate and launch the Gmail URL
            _browserDriver.Navigate().GoToUrl("https://mail.google.com/");

            // Enter the Username
            IWebElement userElement = _browserDriver.FindElement(By.Id(_config["_objectUserNameTextBox"]));
            userElement.SendKeys(_config["username"]);

            // Click on the Next Button
            _browserDriver.FindElement(By.Id(_config["_objectUserNameNextButton"])).Click();

            // Enter the Password
            IWebElement passwordElement = _browserDriver.FindElement(By.Name(_config["_objectPasswordTextBox"]));
            if (passwordElement.Displayed && passwordElement.Enabled)
                passwordElement.SendKeys(_config["password"]);

            // Click on the Next Button
            _browserDriver.FindElement(By.Id(_config["_objectUserPasswordNextButton"])).Click();

            // Click on the Compose Button
            IWebElement composeElement = _browserDriver.FindElement(By.XPath(_config["_objectComposeButton"]));
            composeElement.Click();

            // Enter the Recipient's Email Address
            IWebElement toElement = _browserDriver.FindElement(By.Name(_config["_objectToTextBox"]));
            toElement.Clear();
            toElement.SendKeys($"{_config["username"]}@gmail.com");

            // Click on the Send Mail Button
            IWebElement sendButtonElement = _browserDriver.FindElement(By.XPath(_config["_objectSendMailButton"]));
            sendButtonElement.Click();

            // Handle and Accept the 'Empty Body Text Alert'
            _browserDriver.SwitchTo().Alert().Accept();

            // Validate and Click on the Message Sent Link Text
            IWebElement _messageSentLink = _browserDriver.FindElement(By.CssSelector(_config["_objectMessageSentLink"]));
            Assert.True(_messageSentLink.Text == "View message");
            _messageSentLink.Click();

            // Validate the sent mail's Sender Name.
            string _mailFromText = _browserDriver.FindElement(By.CssSelector(_config["_objectVerifyMailSenderText"])).Text;
            Assert.True(_mailFromText == "User Crossover");
        }
    }
}