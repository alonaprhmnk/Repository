using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SnappetTest
{
    internal class LogInPage : BasePage
    {
        WebDriverWait wait;

        public LogInPage(IWebDriver driver) : base (driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public bool IsVisible {
            get
            {
                return Driver.Title.Contains("Snappet Teacher Dashboard");
            }
            internal set { }
        }

        public IWebElement Username => Driver.FindElement(By.Id("UserName"));

        public IWebElement Password => Driver.FindElement(By.Id("Password"));

        public IWebElement SubmitButton => Driver.FindElement(By.CssSelector(".btn.btn-primary"));

        internal void GoTo()
        {
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://teacher.snappet.org/Account/LogOn");
        }

        internal HomePage FillOutFormAndLogin(string username, string password)
        {
            Username.SendKeys(username);
            Password.SendKeys(password);
            SubmitButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".add-first-lesson")));
            return new HomePage(Driver);
        }
    }
}