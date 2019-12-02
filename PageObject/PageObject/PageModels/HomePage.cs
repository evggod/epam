using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace PageObjectLab.PageModels
{
    public class HomePage : Page
    {
        [FindsBy(How = How.Id, Using = "js-auskunft-autocomplete-from")]
        public IWebElement WhereFromInput;

        [FindsBy(How = How.Id, Using = "js-auskunft-autocomplete-to")]
        public IWebElement WhereToInput;

        [FindsBy(How = How.ClassName, Using = "hasDatepicker")]
        public IWebElement CalendarInput;

        [FindsBy(How = How.Id, Using = "js-auskunft-timeinput")]
        public IWebElement TimePicker;

        
        public HomePage(IWebDriver driver) : base(driver) { }

        public void OpenCalendar()
        {
            WhereFromInput.Click();
            CalendarInput.Click();
        }

        public void OpenTimePicker()
        {
            WhereFromInput.SendKeys("Berlin");
            WhereToInput.SendKeys("Aalen");
            Body.Click();
            Body.Click();
            TimePicker.Click();
        }

        public IWebElement GetNextDayInFuture()
        {
            return GetWebElement($".//*[@class='allDays']//button[text()='{DateTime.Now.AddDays(1).Day}']");
        }

        public IWebElement GetMidnightTime()
        {
            return GetWebElement(".//*[@id='TP-outboundTime']/option");
        }

        public IWebElement GetSubmitButton()
        {
            return GetWebElement(".//*[@class='_bcaf5336 _d49aa3d6']");
        }

        public void SwitchToLastMonthToChoose()
        {
            var nextMonthButton = GetWebElement(".//*[@class='_65dc714d']");
            for (int i = 0; i < 4; i++)
                nextMonthButton.Click();
        }
    }
}
