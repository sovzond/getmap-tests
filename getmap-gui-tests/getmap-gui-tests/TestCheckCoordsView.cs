using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using OpenQA.Selenium;
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
        private const int numberCoords = 1;
        private const int coordMonitorX = 730;
        private const int coordMonitorY = 60;
        private const string textDecimal = " Широта:";
        private const string textMetres = " X:";
        private const string locationMiddle = "#map";
        private const string locationDivTextCoord = "#dCoord div";
        private IWebElement elementForMove;
        private IList<IWebElement> listDivTextCoord;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            listDivTextCoord = driver.FindElements(By.CssSelector(locationDivTextCoord));
        }

        /// <summary>
        /// Проверяет корректное отображение координат в виде десятичных градусов.
        /// </summary>
        [TestMethod]
        public void CheckCoords()
        {
            CheckDecimalCoords();
            CheckMetresCoords();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckDecimalCoords()
        {
            elementForMove = driver.FindElement(By.CssSelector(locationMiddle));
            var builder = new Actions(driver);
            builder.MoveToElement(elementForMove, coordMonitorX, coordMonitorY).ClickAndHold().MoveByOffset(0, 0).Release().Perform();
            GUI.CoordsXY.get(driver).DecimalClick();
            Assert.IsTrue(listDivTextCoord[numberCoords].Text.StartsWith(textDecimal), "После перевода координат в систему счисления 'Десятичные градусы' , они не перевелись.");
        }

        private void CheckMetresCoords()
        {
            System.Threading.Thread.Sleep(1000);
            GUI.CoordsXY.get(driver).MetresClick();
            Assert.IsTrue(listDivTextCoord[numberCoords].Text.StartsWith(textMetres), "После перевода координат в систему счисления 'Метры' , они не перевелись.");
        }

    }

}