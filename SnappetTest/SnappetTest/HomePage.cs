using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SnappetTest
 {
    internal class HomePage : BasePage
    {
        WebDriverWait wait;
        public HomePage(IWebDriver driver) : base(driver)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public void Wait(string selector)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector)));
        }

        public bool IsVisible => Driver.Title.Contains("#Challenge3 - Snappet Teacher Dashboard");

        public IWebElement ActivateBtn => Driver.FindElement(By.ClassName("add-first-lesson"));

        public IWebElement DropDown => Driver.FindElement(By.CssSelector("div.form-group .select2-selection"));

        public IWebElement SelectSubject => Driver.FindElement(By.CssSelector(".select2-results>ul>li:nth-of-type(2)"));

        public IWebElement NextBtn => Driver.FindElement(By.CssSelector("div.align-right .btn-primary"));

        public IWebElement Groep => Driver.FindElement(By.CssSelector("div.form-group .grade-slider-tick"));

        public IWebElement OtherOptions => Driver.FindElement(By.CssSelector(".button-bar .show-more"));
        
        public IWebElement Plan => Driver.FindElement(By.CssSelector(".card.default"));

        public IWebElement Name => Driver.FindElement(By.CssSelector("div.form-group>input"));

        public IWebElement FinalizeSubjectCreation => Driver.FindElement(By.CssSelector("div.button-bar .btn-primary"));

        public IWebElement EditButton => Driver.FindElement(By.CssSelector(".button-bar.left>a"));

        public IWebElement SubjectName => Driver.FindElement(By.CssSelector(".form-group>input"));

        public IWebElement SaveButton => Driver.FindElement(By.CssSelector("div.button-bar button.btn-primary"));

        public IWebElement GradeDropDown => Driver.FindElement(By.CssSelector(".select2-selection"));

        public IWebElement SelectGrade => Driver.FindElement(By.CssSelector(".select2-results>ul>li:nth-of-type(4)"));

        public IWebElement PlanDropDown => Driver.FindElement(By.CssSelector("div.form-group a.btn-more"));

        public IWebElement SelectPlan => Driver.FindElement(By.CssSelector(".popover-content>ul>li:nth-of-type(7)"));

        public IWebElement DeleteButton => Driver.FindElement(By.CssSelector("div.bottom-button-bar button.btn-txt-delete"));

        public IWebElement RemoveButton => Driver.FindElement(By.CssSelector("div.delete-confirm div.button-bar button.btn-primary"));

        public IWebElement CreatedSubjectName => Driver.FindElement(By.CssSelector("div.alternate .panel-card-heading-text-inner .elipsis-1-lines"));
  
        public IWebElement DefaultSet => Driver.FindElement(By.CssSelector(".btn-label"));

        internal void ActivateNewSubject(TestSubject subject)
        {
            ActivateBtn.Click();
            Wait(".jquery-modal.blocker.current");
            DropDown.Click();
            SelectSubject.Click();
            NextBtn.Click();

            Wait("div.form-group .grade-slider-tick");
            Groep.Click();
            NextBtn.Click();

            //open more options for the Groep 3
            Wait(".button-bar .show-more");
            OtherOptions.Click();

            Wait(".cards-container");
            Plan.Click();

            Wait("div.button-bar .btn-primary");
            Name.Clear();
            Name.SendKeys(subject.Name);
            FinalizeSubjectCreation.Click();
        }

        internal void ModifySubject(TestSubject subject)
        {
            Wait(".panel-card.alternate");
            EditButton.Click();
            SubjectName.Clear();
            SubjectName.SendKeys(subject.Name);
            GradeDropDown.Click();
            SelectGrade.Click();
            PlanDropDown.Click();
            SelectPlan.Click();
            SaveButton.Click();
        }

        internal void RemoveSubject()
        {
            Wait(".panel-card.alternate");
            EditButton.Click();
            DeleteButton.Click();
            RemoveButton.Click();
        }
        
        internal void AssertSubjectCreated()
        {
            Wait("div.alternate .panel-card-heading-text-inner .elipsis-1-lines");
            Assert.IsTrue(CreatedSubjectName.Text == "Test Subject", "Subject was not activated.");
        }

        internal void AssertSubjectEdited()
        {
            Wait("div.alternate .panel-card-heading-text-inner .elipsis-1-lines");
            Assert.IsTrue(CreatedSubjectName.Text == "Updated Subject", "Subject was not edited.");
        }

        internal void AssertSubjectDeleted()
        {
            Wait(".btn-primary");
            Assert.IsTrue(DefaultSet.Text == "Activate subject", "Subject was not deleted.");
        }
    }
}