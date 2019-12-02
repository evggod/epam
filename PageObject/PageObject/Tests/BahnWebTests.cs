using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using PageObjectLab.PageModels;

namespace PageObjectLab.Tests
{
    public class Tests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new EdgeDriver()
            {
                Url = "https://www.bahn.com/en"
            };
        }

        [Test]
        public void CheckMaxDateToChoose()
        {
            var homePage = new HomePage(_webDriver);
            homePage.OpenCalendar();
            homePage.SwitchToLastMonthToChoose();
            var nextDayAfterFourMonth = homePage.GetNextDayInFuture();
            Assert.IsFalse(isEnable(nextDayAfterFourMonth));
        }

        [Test]
        public void CheckPastTimeToChoose()
        {
            var homePage = new HomePage(_webDriver);
            var departuriesViewPage = new DeparturiesViewPage(_webDriver);
            homePage.OpenTimePicker();
            var midnight = homePage.GetMidnightTime();
            midnight.Click();
            var submitBtn = homePage.GetSubmitButton();
            submitBtn.Click();
            System.Threading.Thread.Sleep(3000);
            var firstDeparture = departuriesViewPage.GetEarlierDeparture();
            firstDeparture.Click();
            var departurePassedMessage = departuriesViewPage.GetDeparturePassedMessage();
            Assert.IsTrue(departurePassedMessage.Displayed && departurePassedMessage.Text.Contains("Departure time passed"));
        }

        private bool isEnable(IWebElement element)
        {
            return element.GetAttribute("class").Contains("_42bb7915");
        }

        [TearDown]
        public void QuitDriver()
        {
            _webDriver.Quit();
        }
    }
}