using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SnappetTest
{
    [TestClass]
    public class SnappetTests
    {
        private IWebDriver Driver { get; set; }

        [TestInitialize]
        public void LogIn()
        {
            Driver = GetChromeDriver();
            var logInPage = new LogInPage(Driver);
            logInPage.GoTo();
            Assert.IsTrue(logInPage.IsVisible, "Wrong URL to the site was provided.");

            var homePage = logInPage.FillOutFormAndLogin("ChallengeTeacher3_1", "*8jdfD%^st0");
            Assert.IsTrue(homePage.IsVisible, "Login to the site was unsuccessful.");
        }

        [TestMethod]
        public void ActivateSubject()
        {
            var testSubject = new TestSubject();
            testSubject.Name = "Test Subject";
            var homePage = new HomePage(Driver);
            homePage.ActivateNewSubject(testSubject);
            homePage.AssertSubjectCreated();
        }

        [TestMethod]
        public void EditSubject()
        {
            var testSubject = new TestSubject();
            testSubject.Name = "Updated Subject";
            var homePage = new HomePage(Driver);
            homePage.ModifySubject(testSubject);
            homePage.AssertSubjectEdited();
        }

        [TestMethod]
        public void DeleteSubject()
        {
            var homePage = new HomePage(Driver);
            homePage.RemoveSubject();
            homePage.AssertSubjectDeleted();
        }

        private IWebDriver GetChromeDriver()
        {
            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ChromeDriver(outputDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
