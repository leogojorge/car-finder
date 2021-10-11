using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using Xunit;

namespace CarFinder.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IWebDriver webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://www.keplaca.com/");

            IWebElement plateInputElement = webDriver.FindElement(By.Id("sPlaca"));

            plateInputElement.SendKeys("LIS6088");

            IWebElement searchButtonElement = webDriver.FindElement(By.XPath("//input"));
            searchButtonElement.Submit();

            IWebElement carInfosTableElement = webDriver.FindElement(By.XPath("//table"));

            IList<IWebElement> tableRow = carInfosTableElement.FindElements(By.TagName("tr"));
            IList<IWebElement> rowTD;
            foreach (IWebElement row in tableRow)
            {
                rowTD = row.FindElements(By.TagName("td"));

                string column = rowTD[0].Text;
                string value = rowTD[0].Text;

                Console.WriteLine($"{column} {value}");
            }
        }
    }
}
