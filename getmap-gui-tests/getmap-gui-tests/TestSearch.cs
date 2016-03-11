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
        private const string locationResultSearchPanel = "#resultDiv";
        private const string locationCloseButton = "#closeResBtn";
        private const int idxForSplitText = 19;
        private enum NumberSearchAttribute
        {
            Fakel = 0,
            Ambar = 1,
            Places = 2,
            Dns = 3
        }
              
        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            arrayForSearch = new string[4];
            arrayForSearch[(int)NumberSearchAttribute.Fakel] = "Факелы";
            arrayForSearch[(int)NumberSearchAttribute.Ambar] = "Амбары";
            arrayForSearch[(int)NumberSearchAttribute.Places] = "Кустовые площадки";
            arrayForSearch[(int)NumberSearchAttribute.Dns] = "ДНС";

        }

        /// <summary>
        /// Выполняет проверку на поиск, проверяет независимость регистра.
        /// </summary>
        [TestMethod]
        public void CheckSearch()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            if (!GUI.Layers.get(driver).GetSelectedNeftyStruct)
                GUI.Layers.get(driver).NeftyStructCheckBoxClick();
            MakeSearch("фаКеЛ");
            MakeSearch("аМбаР");
            MakeSearch("доЖимНая наСосная стАнция");
            MakeSearch("куСтоВая плоЩадка");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void MakeSearch(string attributeSearch)
        {         
            System.Threading.Thread.Sleep(1000);
            GUI.HeaderLinks.get(driver).MakeSearch(attributeSearch);
            System.Threading.Thread.Sleep(5000);
            IWebElement elementResultSearchPanel = driver.FindElement(By.CssSelector(locationResultSearchPanel));
            string fullTextResultSearch = elementResultSearchPanel.Text;
            if (fullTextResultSearch == "")
                Assert.Fail("Вы ввели искомый элемент: " + fullTextResultSearch + " , но строка поиска осталась пустой.");
            string splitedTextResultSearch = fullTextResultSearch.Remove(0, idxForSplitText);           
            for (int i = 0; i < arrayForSearch.GetLength(0); i++)
            {
                if (!(splitedTextResultSearch == arrayForSearch[(int)NumberSearchAttribute.Fakel] ||
                    splitedTextResultSearch == arrayForSearch[(int)NumberSearchAttribute.Ambar] ||
                    splitedTextResultSearch == arrayForSearch[(int)NumberSearchAttribute.Places] ||
                    splitedTextResultSearch == arrayForSearch[(int)NumberSearchAttribute.Dns]))
                    Assert.Fail("Искомый элемент отсутсвует на сайте: " + arrayForSearch[i]);
            }
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector(locationCloseButton)).Click();
        }
    }
}
