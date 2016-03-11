using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку инструмента 'Копирайт'.
    /// </summary>
    [TestClass]
    public class TestCopyright
    {
        private IWebDriver driver;
        private IWebElement elementTextArea; 
        private const string textRosreestr = "© Росреестр, 2010-2016";
        private const string textGibrid = "Изображения © DigitalGlobe, 2016, Картографические данные (с) Google, 2016";
        private const string textSputnik = "Изображения © DigitalGlobe, 2016";
        private const string textScheme = "Картографические данные © Google, 2016";
        private const string locationTitles = "div.containerTitle h6";
        private const string locationButtonCopyright = "map_over";
        private const string locationTextArea = "div.copyrightText";
        private const string textOSM = "© Участники OpenStreetMap";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        /// <summary>
        /// Перебирает  слои и проверяет, верно ли отображен текст в окне 'КОПИРАЙТЫ'.
        /// </summary>
        [TestMethod]
        public void CheckAreaInCopyright()
        {
            driver.FindElement(By.Name(locationButtonCopyright)).Click();
            Assert.IsTrue(IsHaveBoxShadow(), "Окно 'КОПИРАЙТЫ' отсутствует после клика по инструменту 'Копирайты'.");
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(500);
            GUI.SlideMenu.get(driver).OpenBaseLayers().OpenGoogle();
            CheckTextCopyright("OpenStreetMap");
            CheckTextCopyright("Росреестр");
            CheckTextCopyright("Гибрид");
            CheckTextCopyright("Спутник");
            CheckTextCopyright("Схема");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private Boolean IsHaveBoxShadow()
        {
            IList<IWebElement> listTitles = driver.FindElements(By.CssSelector(locationTitles));
            foreach (var element in listTitles)
            {
                if (element.Text == "КОПИРАЙТЫ")
                    return true;
            }
            return false;
        }

        private void CheckTextCopyright(string text)
        {
            if (text == "OpenStreetMap")
                GUI.SlideMenu.get(driver).OpenStreetMapClick();
            if (text == "Росреестр")
                GUI.SlideMenu.get(driver).RosreestrClick();
            if (text == "Гибрид")
                GUI.SlideMenu.get(driver).LayerGibridClick();
            if (text == "Спутник")
                GUI.SlideMenu.get(driver).LayerSputnikClick();
            if (text == "Схема")
                GUI.SlideMenu.get(driver).LayerSchemeClick();
            elementTextArea = driver.FindElement(By.CssSelector(locationTextArea));
            if(text == "OpenStreetMap")
                Assert.AreEqual(textOSM, elementTextArea.Text, "Окно 'КОПИРАЙТЫ' отобразило неверный текст слоя 'OpenStreetMap'.");
            if (text == "Росреестр")
                Assert.AreEqual(textRosreestr, elementTextArea.Text, "Окно 'КОПИРАЙТЫ' отобразило неверный текст слоя 'Росреестр'.");
            if (text == "Гибрид")
                Assert.AreEqual(textGibrid, elementTextArea.Text, "Окно 'КОПИРАЙТЫ' отобразило неверный текст слоя 'Гибрид' .");
            if (text == "Спутник")
                Assert.AreEqual(textSputnik, elementTextArea.Text, "Окно 'КОПИРАЙТЫ' отобразило неверный текст слоя 'Спутник' .");
            if (text == "Схема")
                Assert.AreEqual(textScheme, elementTextArea.Text, "Окно 'КОПИРАЙТЫ' отобразило неверный текст слоя 'Схема' .");
        }

    }
    
}
