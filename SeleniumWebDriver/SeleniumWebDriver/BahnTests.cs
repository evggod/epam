using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;

namespace Tests
{
    public class Tests
    {
        private SafariDriver safari;

        [SetUp]
        public void SetupChromeDriver()
        {
			safari = new SafariDriver();
        }

        [Test]
		public void SearchRouteTest()
		{
			safari.Navigate().GoToUrl("https://www.bahn.com/en");
			safari.FindElement(By.Id("js-auskunft-autocomplete-from")).SendKeys("Berlin");
			safari.FindElement(By.Id("js-auskunft-autocomplete-to")).SendKeys("Aalen");
			safari.FindElement(By.ClassName("js-submit-btn")).SendKeys(Keys.Enter);
			var ResultContent = safari.FindElementByClassName("resultContentHolder").Enabled;
			Assert.IsTrue(ResultContent);
		}

		[Test]
		public void LogInTest()
		{
			safari = new SafariDriver();
			safari.Navigate().GoToUrl("https://www.bahn.de/p/view/meinebahn/login.shtml");
			safari.FindElement(By.Id("Benutzername")).SendKeys("gaga231772");
			safari.FindElement(By.Id("Passwort")).SendKeys("gaga2317");
			safari.FindElement(By.ClassName("btn")).SendKeys(Keys.Enter);
			var username = safari.FindElementByClassName("nobttommargin").FindElement(By.TagName("span")).GetProperty("title");
			bool isUsername = false;
			if (username == "Evgenij Bondarik")
			{
				isUsername = true;
			}
			else isUsername = false;
			Assert.IsTrue(isUsername);

		}

        [TearDown]
        public void QuitDriver()
        {
            safari.Quit();
            safari.Dispose();
        }
    }
}
