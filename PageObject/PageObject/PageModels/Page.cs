using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectLab.PageModels
{
    public abstract class Page
    {
        protected readonly IWebDriver _webDriver;

        [FindsBy(How = How.TagName, Using = "body")]
        public IWebElement Body;

        public Page(IWebDriver driver)
        {
            _webDriver = driver;
            PageFactory.InitElements(_webDriver, this);
        }

        public IWebElement GetWebElement(string xPath)
        {
            return _webDriver.FindElement(By.XPath(xPath));
        }
    }
}
