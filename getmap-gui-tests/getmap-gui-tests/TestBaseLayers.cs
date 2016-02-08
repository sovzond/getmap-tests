using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using GetMapTest.GUI;

namespace GetMapTest
{
    /// <summary>
    ///Выполняет проверку на отображение слоев гугл, а так же на автоматическое включение  слоя OpenStreetMap  
    /// </summary>
    [TestClass]
    public class TestBaseLayers
    {
        private IWebDriver driver;
        private const string urlImageScheme = "http://maps.googleapis.com/maps/api/js/StaticMapService.GetMapImage?1m2&1i363301&2i149508&2e1&3u11&4m2&1u1426&2u595&5m5&1e0&5sru-RU&6sus&10b1&12b1&token=21311";
        private const string urlImageSputnik = "http://maps.googleapis.com/maps/api/js/StaticMapService.GetMapImage?1m2&1i363301&2i149508&2e2&3u11&4m2&1u1426&2u595&5m5&1e2&5sru-RU&6sus&10b1&12b1&token=32241";
        private const string urlImageGibrid = "http://maps.googleapis.com/maps/api/js/StaticMapService.GetMapImage?1m2&1i363301&2i149508&2e2&3u11&4m2&1u1426&2u595&5m5&1e3&5sru-RU&6sus&10b1&12b1&token=98410";
        private const string locationSlideMenu = "#menuSlide div.svzSimpleButton.slidePanelButton";
        private const string locationBaseLayers = "#layersCon div.svzSimpleButton.accordionButton";
        private const string locationBaseLayersChildContainer = "layerManagerBasemap";
        private const string locationRadioButtonOSM = "#layerManagerBasemap div.dijit.dijitReset.dijitInline.dijitRadio.dijitRadioChecked.dijitChecked input";
        private const string locationGoogle = "#stdportal_LayerManagerBase_0 div.svzLayerManagerText";
        private const string locationLayerSputnik = "dijit_form_RadioButton_1";
        private const string locationLayerScheme = "#stdportal_LayerManagerBase_0 input.dijitReset.dijitCheckBoxInput";
        private const string locationLayerGibrid = "dijit_form_RadioButton_2";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
        }

        /// <summary>
        ///Выполняет проверку на отображение слоев гугл, а так же на автоматическое включение  слоя OpenStreetMap
        ///</summary>
        [TestMethod]
        public void CheckBaseLayers()
        {
            LogOn();
            OpenBaseLayers();
            CheckSelectedOSM();
            OpenGoogle();
            CheckLayerScheme();
            CheckLayerSputnik();
            CheckLayerGibrid();
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
        private void OpenBaseLayers()
        {
            driver.FindElement(By.CssSelector(locationSlideMenu)).Click();
            IWebElement elementBaseLayer = driver.FindElement(By.CssSelector(locationBaseLayers));
            var builderBaseLayers = new Actions(driver);
            System.Threading.Thread.Sleep(1000);
            builderBaseLayers.Click(elementBaseLayer).Perform();
            System.Threading.Thread.Sleep(1000);
        }
        private void CheckSelectedOSM()
        {
            IWebElement elementRadioButtonTrue = driver.FindElement(By.Id(locationBaseLayersChildContainer)).
              FindElement(By.XPath("//input[@aria-checked='true']"));
            IWebElement elementOSM = driver.FindElement(By.CssSelector(locationRadioButtonOSM));
            Assert.AreEqual(elementOSM.Location.X, elementRadioButtonTrue.Location.X, "Базовый слой OpenStreetMap отключен");
            Assert.AreEqual(elementOSM.Location.Y, elementRadioButtonTrue.Location.Y, "Базовый слой OpenStreetMap отключен");
        }
        private void OpenGoogle()
        {
            driver.FindElement(By.CssSelector(locationGoogle)).Click();
            System.Threading.Thread.Sleep(1000);
        }
        private void CheckLayerScheme()
        {
            driver.FindElement(By.Id(locationLayerSputnik)).Click();
            driver.FindElement(By.CssSelector(locationLayerScheme)).Click();
            System.Threading.Thread.Sleep(1000);
            IList<IWebElement> elementScheme = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            Assert.AreEqual(urlImageScheme, elementScheme[0].GetAttribute("src"), "Слой схема отобразил не корректный слой.");
        }
        private void CheckLayerSputnik()
        {
            driver.FindElement(By.Id(locationLayerSputnik)).Click();
            System.Threading.Thread.Sleep(1000);
            IList<IWebElement> elementSputnik = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            Assert.AreEqual(urlImageSputnik, elementSputnik[0].GetAttribute("src"), "Слой спутник отобразил не корректный слой");
        }
        private void CheckLayerGibrid()
        {
            driver.FindElement(By.Id(locationLayerGibrid)).Click();
            System.Threading.Thread.Sleep(1000);
            IList<IWebElement> elementGibrid = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            Assert.AreEqual(urlImageGibrid, elementGibrid[0].GetAttribute("src"), "Слой гибрид отобразил не корректный слой");
        }

    }
}
