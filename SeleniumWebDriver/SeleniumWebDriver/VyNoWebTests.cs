using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Tests
{
    public class VyNoWebTests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void SetupChromeDriver()
        {
            webDriver = new EdgeDriver() { Url = "https://www.vy.no/en" };
        }

        [Test]
        public void CheckFindingRoutesWithEmptyFields()
        {
            var whereFrom_inputField = GetWebElement(".//*[@id='TP-departureStation']");
            whereFrom_inputField.SendKeys("some text");
            whereFrom_inputField.Clear();
            var errorMessage = GetWebElement(".//*[@id='submitTravelPlanner']/h2");
            var submit_button = GetWebElement(".//*[@class='_99266285 _d49aa3d6']");
            var isErrorTextCorrect = errorMessage.Text.Equals("Please complete your selection above.");
            Assert.IsTrue(IsElementDisable(submit_button) && errorMessage.Displayed && isErrorTextCorrect);
        }

        [Test]
        public void CheckPassangerMaxNumber()
        {
            var whereFrom_inputField = GetWebElement(".//*[@id='TP-departureStation']");
            var whereTo_inputField = GetWebElement(".//*[@id='TP-arrivalStation']");
            whereFrom_inputField.SendKeys("Oslo S");
            whereTo_inputField.SendKeys("Stavanger");
            var incrementAdult_button = GetWebElement(".//*[@id='center']/div[1]/div[2]/div[1]/form[1]/div[1]/div[3]/div[3]/ul[1]/li[1]/span[1]/button[2]");
            var body = GetWebElement(".//body[1]");
            WebElementClick(body, 2);
            WebElementClick(incrementAdult_button, 9);
            System.Threading.Thread.Sleep(500);
            var errorMessage = GetWebElement(".//*[@id='TP-errorAdult']");
            var incrementAdultDisable_button = GetWebElement(".//*[@id='center']/div[1]/div[2]/div[1]/form[1]/div[1]/div[3]/div[3]/ul[1]/li[1]/span[1]/button[2]");
            var isErrorTextCorrect = errorMessage.Text.Contains("It is only possible to book for up to 9 passengers online. For group travel, please contact customer service at");

            Assert.IsTrue(IsElementDisable(incrementAdultDisable_button) && errorMessage.Displayed && isErrorTextCorrect);
        }

        private IWebElement GetWebElement(string xPath)
        {
            return webDriver.FindElement(By.XPath(xPath));
        }

        private void WebElementClick(IWebElement element, int count)
        {
            for (int i = 0; i < count; i++)
            {
                element.Click();
            }
        }

        private bool IsElementDisable(IWebElement element)
        {
            var backColor = element.GetCssValue("background-color");
            return backColor.Contains("rgba(239, 239, 239, 1)") || backColor.Contains("#EFEFEF") || backColor.Contains("rgb(239, 239, 239)");
        }

        [TearDown]
        public void QuitDriver()
        {
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}
