using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;

namespace GetMapTest
{
    /// <summary>
    /// Осуществляет проверку на изменение порядка отображения слоев.
    /// </summary>
    [TestClass]
    public class TestChangeSortLayers
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        private string zIndexDNS;
        private string zIndexFakel;
        private string zIndexAmbar;
        private string zIndexPlaces;
        private IList<IWebElement> listButtonsIncDec;
        private const string locationButtonsIncDec = "span.move img";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            js = driver as IJavaScriptExecutor;
            zIndexDNS = "";
            zIndexFakel = "";
            zIndexAmbar = "";
            zIndexPlaces = "";
        }

        /// <summary>
        ///Перемещает каждый последний слой на позицию первого.
        ///</summary>
        [TestMethod]
        public void СheckIncrementLayers()
        {
            DataPreparation();
            IncrementLayerDNS();
            IncrementLayerPlaces();
            IncrementLayerAmbar();
            IncrementLayerFakel();
        }

        [TestCleanup]
        public void Clean()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }

        private void DataPreparation()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            if (!GUI.Layers.get(driver).GetSelectedNeftyStruct)
                GUI.Layers.get(driver).NeftyStructCheckBoxClick();
            GUI.SlideMenu.get(driver).OpenLegenda();
            listButtonsIncDec = driver.FindElements(By.CssSelector(locationButtonsIncDec));
        }

        private void IncrementLayerDNS()
        {
            zIndexFakel = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Факелы\")[0].div.style.zIndex;");
            for (int i = 0; i < 3; i++)
                listButtonsIncDec[6].Click();
            zIndexDNS = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_ДНС\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexFakel, zIndexDNS, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }

        private void IncrementLayerPlaces()
        {
            for (int i = 0; i < 3; i++)
                listButtonsIncDec[4].Click();
            zIndexPlaces = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Кустовые площадки\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexDNS, zIndexPlaces, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }

        private void IncrementLayerAmbar()
        {
            for (int i = 0; i < 3; i++)
                listButtonsIncDec[2].Click();
            zIndexAmbar = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Амбары\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexPlaces, zIndexAmbar, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }

        private void IncrementLayerFakel()
        {
            for (int i = 0; i < 3; i++)
                listButtonsIncDec[0].Click();
            zIndexFakel = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Факелы\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexPlaces, zIndexFakel, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
    }
}
