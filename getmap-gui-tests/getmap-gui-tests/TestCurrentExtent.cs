 using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GetMapTest
{
    /// <summary>
    /// Проверяет различными способами изменение экстента, сравнение измененного экстента, с текущем.   
    /// </summary>
    [TestClass]
    public class TestCurrentExtent
    {
        private IWebDriver driver;
        private const string locationTextArea = "#linkExtent textarea";
        private const string locationElementForMove = "#map";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");           
        }

        /// <summary>
        ///Проверяет отображение окна "Ссылка" и осуществляет сравнение экстентов.
        ///</summary>
        [TestMethod]
        public void CheckButtonLink()
        {
            ButtonLink();
        }

        /// <summary>
        /// Проверяет после клика по кнопке 'Полный экстент' , что система отобразила тот же экстент карты, который отображается при входе.
        /// </summary>
        [TestMethod]
        public void CheckButtonFullExtent()
        {
            ButtonFullExtent();
        }

        /// <summary>
        /// Перемещает карту, а потом выполняет клик по кнопке 'Предыдущий экстент', затем сравнивает экстенты до перемещения и после нажатия по кнопке 'Предыдущий экстент'.
        /// </summary>
        [TestMethod]
        public  void CheckBackButton()
        {
            BackButton();
        }

        /// <summary>
        /// Проверяет отображен тот же экстент карты, что и до перехода к предыдущему экстенту.
        /// </summary>
        [TestMethod]
        public void CheckNextButton()
        {
            NextButton();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void ButtonLink()
        {
            GUI.MenuNavigation.get(driver).GotoCoordsButton();
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
            GUI.MenuDop.get(driver).OpenLinks();
            IWebElement elementTextArea = driver.FindElement(By.CssSelector(locationTextArea));
            string fullLink = elementTextArea.Text;
            int idxExtentCoords = fullLink.IndexOf('=');
            idxExtentCoords++;
            string onlyExtentCoords = fullLink.Substring(idxExtentCoords);
            string[] splitedExtentCoords = onlyExtentCoords.Split(',');
            string[] splitedExtentCoordsCurrent = GUI.GetExtents.get(driver).GetCurrentExtent;
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(splitedExtentCoordsCurrent[i], splitedExtentCoords[i], "Текущий экстент карты не совпадает с ссылкой на экстент в текстовом поле.");
            GUI.Cleanup.get(driver).Close();
        }

        private void ButtonFullExtent()
        {
            Settings.Instance.Open(driver, Settings.Instance.BaseUrl);
            string[] BaseExtentCoords = GUI.GetExtents.get(driver).GetBaseExtent;
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
            GUI.MenuNavigation.get(driver).FullExtentButton();
            string[] CurrentExtentCoords = GUI.GetExtents.get(driver).GetCurrentExtent;
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(BaseExtentCoords[i], CurrentExtentCoords[i], "После клика по кнопке 'Полный экстент' экстент перестал совпадать с тем, который отображается при входе. ");
            GUI.Cleanup.get(driver).Close();
        }

        private void BackButton()
        {
            Settings.Instance.Open(driver, Settings.Instance.BaseUrl);
            string[] BaseExtentCoords = GUI.GetExtents.get(driver).GetCurrentExtent;
            GUI.MenuNavigation.get(driver).MoveButton();
            IWebElement elementForMove = driver.FindElement(By.CssSelector(locationElementForMove));
            var builder = new Actions(driver);
            builder.MoveToElement(elementForMove, 731, 60).ClickAndHold().Perform();
            builder.MoveToElement(elementForMove, 800, 40).Release().Perform();
            GUI.MenuNavigationHistory.get(driver).Back();
            string[] CurrentExtentCoords = GUI.GetExtents.get(driver).GetCurrentExtent;
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(BaseExtentCoords[i], CurrentExtentCoords[i], "После клика по кнопке 'Предыдущий экстент' экстент перестал совпадать с тем, который был до перемещения карты. ");
            GUI.Cleanup.get(driver).Close();
        }

        private void NextButton()
        {
             Settings.Instance.Open(driver, Settings.Instance.BaseUrl);
            string[] BaseExtentCoords = GUI.GetExtents.get(driver).GetBaseExtent;
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
            GUI.MenuNavigationHistory.get(driver).Back().Next();
            string[] CurrentExtentCoords = GUI.GetExtents.get(driver).GetCurrentExtent;
            for (int i = 0; i < 4; i++)
                Assert.AreEqual(BaseExtentCoords[i], CurrentExtentCoords[i], "После клика по кнопке 'Слующий экстент' экстент перестал совпадать с тем, который был до клика по кнопке 'Предыдущий экстент' .");
            GUI.Cleanup.get(driver).Close();
        }




    }
}
