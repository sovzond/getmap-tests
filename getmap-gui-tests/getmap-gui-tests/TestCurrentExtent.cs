using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Осуществляет работу с экстентом, проводит сравнивание экстентов.
    /// </summary>
    [TestClass]
    public class TestCurrentExtent
    {
        private IWebDriver driver;
        private const string locationButtonXY = "#menuNavigation div.svzSimpleButton.gotoCoordsButton";
        private const string locationButtonLinks = "#menuDop div.svzSimpleButton.linkBtn";
        private const string locationTextArea = "#linkExtent textarea";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
        }
        /// <summary>
        ///Проверяет отображение окна "Ссылка" и осуществляет сравнение экстентов.
        ///</summary>
        [TestMethod]
        public void CheckExtent()
        {
            LogOn();
            MoveExtent();
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

        private void MoveExtent()
        {
            driver.FindElement(By.CssSelector(locationButtonXY)).Click();
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
            driver.FindElement(By.CssSelector(locationButtonLinks)).Click();
            IWebElement elementTextArea = driver.FindElement(By.CssSelector(locationTextArea));
            string fullLink = elementTextArea.Text;
            int idxExtentCoords = fullLink.IndexOf('=');
            idxExtentCoords++;
            string onlyExtentCoords = fullLink.Substring(idxExtentCoords);
            string[] splitedExtentCoords = onlyExtentCoords.Split(',');
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            string onlyExtentCoordsCurrent = (string)js.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");
            string[] splitedExtentCoordsCurrent = onlyExtentCoordsCurrent.Split(',');
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(splitedExtentCoordsCurrent[i], splitedExtentCoords[i], "Текущий экстент карты не является корректным");
        }
    }
}
