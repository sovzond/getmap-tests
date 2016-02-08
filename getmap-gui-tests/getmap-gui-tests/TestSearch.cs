using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;


namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку поиска ,а так же независимость от регистра искомого элемента.
    /// </summary>
    [TestClass]
    public class TestSearch
    {
        private IWebDriver driver;
        private string[] arrayForSearch;
        private const string locationSearchArea = "input.searchPanel";
        private const string locationSearchButton = "#textSearch2";
        private const string locationResultSearchPanel = "#resultDiv";
        private const int idxForSplitText = 19;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            arrayForSearch = new string[4];
            arrayForSearch[0] = "Факелы";
            arrayForSearch[1] = "Амбары";
            arrayForSearch[2] = "Кустовые площадки";
            arrayForSearch[3] = "ДНС";
        }

        /// <summary>
        /// Выполняет проверку на поиск, проверяет независимость регистра.
        /// </summary>
        [TestMethod]
        public void CheckSearch()
        {
            LogOn();
            CheckSearch("аМбаР");
        }

        [TestCleanup]
        public void Clean()
        {
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

        private void LogOn()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        private void CheckSearch(string attributeSearch)
        {
            MakeSearch(attributeSearch);
            System.Threading.Thread.Sleep(2000);
            IWebElement elementResultSearchPanel = driver.FindElement(By.CssSelector(locationResultSearchPanel));
            string fullTextResultSearch = elementResultSearchPanel.Text;
            string splitedTextResultSearch = fullTextResultSearch.Remove(0, idxForSplitText);
            for (int i = 0; i < arrayForSearch.GetLength(0); i++)
            {
                if (!(splitedTextResultSearch == arrayForSearch[0] ||
                    splitedTextResultSearch == arrayForSearch[1] ||
                    splitedTextResultSearch == arrayForSearch[2] ||
                    splitedTextResultSearch == arrayForSearch[3]))
                    Assert.Fail("Искомый элемент отсутсвует на сайте.");
            }
        }

        private void MakeSearch(string attributeSearch)
        {
            driver.FindElement(By.CssSelector(locationSearchArea)).Click();
            driver.FindElement(By.CssSelector(locationSearchArea)).SendKeys(attributeSearch);
            driver.FindElement(By.CssSelector(locationSearchButton)).Click();
        }

    }
}
