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
        Utils.TransformJS js;
        private const int Xfirst = 730;
        private const int Yfirst = 60;
        private const int Xsecond = 800;
        private const int Ysecond = 40;
        private const string locationTextArea = "#linkExtent textarea";
        private const string locationElementForMove = "#map";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            js = new Utils.TransformJS(driver);
        }

        /// <summary>
        ///Проверяет отображение окна "Ссылка" и осуществляет сравнение экстентов.
        ///</summary>
        [TestMethod]
        public void CheckButtonLink()
        {
            GUI.MenuNavigation.get(driver).GotoCoordsButton();
            MoveToNeftyStruct();
            GUI.BottomMenu.get(driver).OpenLinks();
            IWebElement elementTextArea = driver.FindElement(By.CssSelector(locationTextArea));
            string[] extentInTextArea = js.SplitExtentFromLink(elementTextArea.Text);
            string[] extentCurrent = js.GetCurrentExtentSplited();
            for (int i = 0; i < extentCurrent.Length; i++)
                Assert.AreEqual(extentInTextArea[i], extentCurrent[i], "Текущий экстент карты не совпадает с ссылкой на экстент в текстовом поле.");
        }

        /// <summary>
        /// Проверяет после клика по кнопке 'Полный экстент' , что система отобразила тот же экстент карты, который отображается при входе.
        /// </summary>
        [TestMethod]
        public void CheckButtonFullExtent()
        {
            string[] baseExtent = js.GetBaseExtentSplited();
            MoveToNeftyStruct();
            GUI.MenuNavigation.get(driver).FullExtentButton();
            string[] currentExtent = js.GetCurrentExtentSplited();
            for (int i = 0; i < baseExtent.Length; i++)
                Assert.AreEqual(currentExtent[i], baseExtent[i], "После клика по кнопке 'Полный экстент' экстент перестал совпадать с тем, который отображается при входе. ");
        }

        /// <summary>
        /// Перемещает карту, а потом выполняет клик по кнопке 'Предыдущий экстент', затем сравнивает экстенты до перемещения и после нажатия по кнопке 'Предыдущий экстент'.
        /// </summary>
        [TestMethod]
        public void CheckBackButton()
        {
            string[] extentBeforeMove = js.GetCurrentExtentSplited();
            GUI.MenuNavigation.get(driver).MoveButton();
            IWebElement elementForMove = driver.FindElement(By.CssSelector(locationElementForMove));
            var builder = new Actions(driver);
            builder.MoveToElement(elementForMove, Xfirst, Yfirst).ClickAndHold().MoveToElement(elementForMove, Xsecond, Ysecond).Release().Perform();
            GUI.MenuNavigationHistory.get(driver).Back();
            string[] extentAfterMove = js.GetCurrentExtentSplited();
            for (int i = 0; i < extentAfterMove.Length; i++)
                Assert.AreEqual(extentBeforeMove[i], extentAfterMove[i], "После клика по кнопке 'Предыдущий экстент' экстент перестал совпадать с тем, который был до перемещения карты. ");
        }

        /// <summary>
        /// Проверяет отображен тот же экстент карты, что и до перехода к предыдущему экстенту.
        /// </summary>
        [TestMethod]
        public void CheckNextButton()
        {
            MoveToNeftyStruct();
            string[] extentBeforeMove = js.GetCurrentExtentSplited();
            GUI.MenuNavigationHistory.get(driver).Back().Next();
            string[] extentAfterMove = js.GetCurrentExtentSplited();
            for (int i = 0; i < extentBeforeMove.Length; i++)
                Assert.AreEqual(extentBeforeMove[i], extentAfterMove[i], "После клика по кнопке 'Слующий экстент' экстент перестал совпадать с тем, который был до клика по кнопке 'Предыдущий экстент' .");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }     

        private void MoveToNeftyStruct()
        {
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(60, 50, 50).click();
        }
    }
}
