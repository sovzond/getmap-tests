
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку корректно отображенного текста копирайта в нижнем правом углу.
    /// </summary>
    [TestClass]
    public class TestCopyright
    {
        private IWebDriver driver;
        private IWebElement elementOSMRosreestr;
        private IWebElement elementsGoolgeLayers;
        private const string textRosreestr = "© Росреестр, 2010-2016";
        private const string textGibrid = "Картографические данные © 2016 Google Изображения ©2016 TerraMetrics";
        private const string textSputnik = "Изображения ©2016 TerraMetrics";
        private const string textScheme = "Картографические данные © 2016 Google";
        private const string textOSM = "© OpenStreetMap contributors";
        private const string locationOSMRosreestr = "div.olControlAttribution.olControlNoSelect";
        private const string locationGoogleLayers = "div.olForeignContainer > div";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        /// <summary>
        /// Перебирает  слои и проверяет, верно ли отображен текст копирайта.
        /// </summary>
        [TestMethod]
        public void CheckAreaInCopyright()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(500);
            GUI.SlideMenu.get(driver).OpenBaseLayers().OpenGoogle();
            AssertOSMRosreestr(textOSM);
            GUI.SlideMenu.get(driver).RosreestrClick();
            AssertOSMRosreestr(textRosreestr);
            GUI.SlideMenu.get(driver).LayerSputnikClick().LayerGibridClick();
            AssertGoogleLayers(textGibrid, 68);
            GUI.SlideMenu.get(driver).LayerSchemeClick();
            AssertGoogleLayers(textScheme, 37);
            GUI.SlideMenu.get(driver).LayerSputnikClick();
            AssertGoogleLayers(textSputnik, 30);
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void AssertOSMRosreestr(string expected)
        {
            elementOSMRosreestr = driver.FindElement(By.CssSelector(locationOSMRosreestr));
            Assert.AreEqual(expected, elementOSMRosreestr.Text, "Внизу справа отобразился неверный текст копирайта");
        }

        private void AssertGoogleLayers(string expected,int count)
        {
            elementsGoolgeLayers = driver.FindElement(By.CssSelector(locationGoogleLayers));
            string substring = elementsGoolgeLayers.Text;
            substring = substring.Substring(0, count);
            Assert.AreEqual(expected, substring, "Внизу справа отобразился неверный текст копирайта");
        }

    }
    
}
