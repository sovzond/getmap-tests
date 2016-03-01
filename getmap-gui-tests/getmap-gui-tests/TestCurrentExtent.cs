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
        public void CheckBackButton()
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
            Utils.XY extentInTextArea = new Utils.XY(fullLink);
            Utils.TransformJS js = new Utils.TransformJS(driver);
            Utils.XY extentCurrent = js.GetCurrentExtent();
                Assert.AreEqual(extentInTextArea, extentCurrent, "Текущий экстент карты не совпадает с ссылкой на экстент в текстовом поле.");
        }

        private void ButtonFullExtent()
        {
            Settings.Instance.Open(driver, Settings.Instance.BaseUrl);
            Utils.TransformJS js = new Utils.TransformJS(driver);
            Utils.XY baseExtent = js.GetBaseExtent();
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
            GUI.MenuNavigation.get(driver).FullExtentButton();
            Utils.XY currentExtent = js.GetCurrentExtent();
                Assert.AreEqual(currentExtent, baseExtent, "После клика по кнопке 'Полный экстент' экстент перестал совпадать с тем, который отображается при входе. ");
        }

        private void BackButton()
        {
            Settings.Instance.Open(driver, Settings.Instance.BaseUrl);
            Utils.TransformJS js = new Utils.TransformJS(driver);
            Utils.XY extentBeforeMove = js.GetCurrentExtent();
            GUI.MenuNavigation.get(driver).MoveButton();
            IWebElement elementForMove = driver.FindElement(By.CssSelector(locationElementForMove));
            var builder = new Actions(driver);
            builder.MoveToElement(elementForMove, 731, 60).ClickAndHold().Perform();
            builder.MoveToElement(elementForMove, 800, 40).Release().Perform();
            GUI.MenuNavigationHistory.get(driver).Back();
            Utils.XY extentAfterMove = js.GetCurrentExtent();
                Assert.AreEqual(extentBeforeMove, extentAfterMove, "После клика по кнопке 'Предыдущий экстент' экстент перестал совпадать с тем, который был до перемещения карты. ");
        }

        private void NextButton()
        {
            Settings.Instance.Open(driver, Settings.Instance.BaseUrl);
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
            Utils.TransformJS js = new Utils.TransformJS(driver);
            Utils.XY extentBeforeMove = js.GetCurrentExtent();
            GUI.MenuNavigationHistory.get(driver).Back().Next();
            Utils.XY extentAfterMove = js.GetCurrentExtent();
                Assert.AreEqual(extentBeforeMove, extentAfterMove, "После клика по кнопке 'Слующий экстент' экстент перестал совпадать с тем, который был до клика по кнопке 'Предыдущий экстент' .");
        }

    }
}
