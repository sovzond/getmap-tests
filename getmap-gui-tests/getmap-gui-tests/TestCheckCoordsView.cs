using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using OpenQA.Selenium;
using GetMapTest.Utils;
using OpenQA.Selenium.Interactions;

namespace GetMapTest
{
    /// <summary>
    /// Проверяет корректное отображение координат в левом нижнем углу экрана.
    /// </summary>
    [TestClass]
    public class TestCheckCoordsView
    {
        private IWebDriver driver;
        private const string locationMiddle = "#map";
        private const string locationCoordsValue = "#dCoord";
        private const string locationElementsP = "#dCoord p";
        private IWebElement elementForMove;
        private IList<IWebElement> listElementP;
   
        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        /// <summary>
        /// Проверяет корректное отображение координат в виде десятичных градусов.
        /// </summary>
        [TestMethod]
        public void TestMeth()
        {
            CheckDecimalCoords();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckDecimalCoords()
        {
            listElementP = driver.FindElements(By.CssSelector(locationElementsP));
            elementForMove = driver.FindElement(By.CssSelector(locationMiddle));
            var builder = new Actions(driver);
            builder.MoveToElement(elementForMove, 730, 60).ClickAndHold().MoveByOffset(0, 0).Release().Perform();
            driver.FindElement(By.CssSelector(locationCoordsValue)).Click();
            System.Threading.Thread.Sleep(1000);
            listElementP[1].Click();
        }
    }
    
}