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
            GUI.SlideMenu.get(driver).OpenLayers();
            if (!GUI.Layers.get(driver).GetSelectedNeftyStruct)
                driver.FindElement(By.CssSelector("#dijit_form_CheckBox_23")).Click();
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

        private void IncrementLayerDNS()
        {
            GUI.SlideMenu.get(driver).OpenLegenda();
            zIndexFakel = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Факелы\")[0].div.style.zIndex;");
            for (int i = 0; i < 3; i++)
                GUI.SlideMenu.get(driver).ButtonIncDNSClick();
            zIndexDNS = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_ДНС\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexFakel, zIndexDNS, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
        private void IncrementLayerPlaces()
        {
            for (int i = 0; i < 3; i++)
                GUI.SlideMenu.get(driver).ButtonIncPlacesClick();
            zIndexPlaces = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Кустовые площадки\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexDNS, zIndexPlaces, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
        private void IncrementLayerAmbar()
        {
            for (int i = 0; i < 3; i++)
                GUI.SlideMenu.get(driver).ButtonIncAmbarClick();
            zIndexAmbar = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Амбары\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexPlaces, zIndexAmbar, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
        private void IncrementLayerFakel()
        {
            for (int i = 0; i < 3; i++)
                GUI.SlideMenu.get(driver).ButtonIncFakelClick();
            zIndexFakel = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"wms_Факелы\")[0].div.style.zIndex;");
            Assert.AreEqual(zIndexPlaces, zIndexFakel, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
    }
}
