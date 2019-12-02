using OpenQA.Selenium;

namespace PageObjectLab.PageModels
{
    public class DeparturiesViewPage : Page
    {
        public DeparturiesViewPage(IWebDriver driver) : base(driver) { }

        public IWebElement GetEarlierDeparture()
        {
            return GetWebElement(".//*[@class='itinerary-list-item'][1]/button");
        }

        public IWebElement GetDeparturePassedMessage()
        {
            return GetWebElement(".//*[@class='itinerary-list-item itinerary-list-item--open']/div[1]/div[2]/p[1]");
        }
    }
}
