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
        private IList<IWebElement> listGoogleImage;
        List<string> srcGoogleImage;
        private const string urlGoogleImage = "http://maps.googleapis.com/maps/api/js/StaticMapService.GetMapImage";
        private const string locationSlideMenu = "#menuSlide div.svzSimpleButton.slidePanelButton";
        private const string locationBaseLayers = "#layersCon div.svzSimpleButton.accordionButton";
        private const string locationBaseLayersChildContainer = "layerManagerBasemap";
        private const string locationRadioButtonOSM = "#layerManagerBasemap div.dijit.dijitReset.dijitInline.dijitRadio.dijitRadioChecked.dijitChecked input";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            listGoogleImage = null;
        }

        /// <summary>
        ///Выполняет проверку на отображение слоев гугл, а так же на автоматическое включение  слоя OpenStreetMap
        ///</summary>
        [TestMethod]
        public void CheckBaseLayers()
        {
            GUI.SlideMenu.get(driver).OpenLayers().OpenBaseLayers();
            CheckSelectedOSM();
            GUI.SlideMenu.get(driver).OpenGoogle();
            GUI.SlideMenu.get(driver).LayerSputnikClick();
            GUI.SlideMenu.get(driver).LayerSchemeClick();
         
            // CheckLayerScheme();
            //  CheckLayerSputnik();
            //  CheckLayerGibrid();
        }

        [TestCleanup]
        public void Clean()
        {
            System.Threading.Thread.Sleep(2000);
            driver.Quit();
        }

        private void CheckSelectedOSM()
        {
            IWebElement elementRadioButtonTrue = driver.FindElement(By.Id(locationBaseLayersChildContainer)).
              FindElement(By.XPath("//input[@aria-checked='true']"));
            IWebElement elementOSM = driver.FindElement(By.CssSelector(locationRadioButtonOSM));
            Assert.AreEqual(elementOSM.Location.X, elementRadioButtonTrue.Location.X, "Базовый слой OpenStreetMap отключен");
            Assert.AreEqual(elementOSM.Location.Y, elementRadioButtonTrue.Location.Y, "Базовый слой OpenStreetMap отключен");
        }

        private void CheckLayerScheme()
        {
            GUI.SlideMenu.get(driver).LayerSputnikClick();
            GUI.SlideMenu.get(driver).LayerSchemeClick();
            IList<IWebElement> elementScheme = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            List<string> src = new List<string>();
            foreach (var r in elementScheme)
                src.Add(r.GetAttribute("src"));
            Assert.AreEqual(urlImageScheme, elementScheme[0].GetAttribute("src"), "Слой схема отобразил не корректный слой.");
        }

        private void CheckLayerSputnik()
        {
            GUI.SlideMenu.get(driver).LayerSputnikClick();
            IList<IWebElement> elementSputnik = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            Assert.AreEqual(urlImageSputnik, elementSputnik[0].GetAttribute("src"), "Слой спутник отобразил не корректный слой");
        }

        private void CheckLayerGibrid()
        {
            GUI.SlideMenu.get(driver).LayerGibridClick();
            IList<IWebElement> elementGibrid = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            Assert.AreEqual(urlImageGibrid, elementGibrid[0].GetAttribute("src"), "Слой гибрид отобразил не корректный слой");
        }

        private void CheckUrlLayers(string layer)
        {
            listGoogleImage = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            List<string> src = new List<string>();
            foreach (var r in listGoogleImage)
                src.Add(r.GetAttribute("src"));
            if (!src[0].StartsWith(urlGoogleImage))
                Assert.Fail("Слой" + layer + "имеет не верных путь изображения.");
        }

    }
}
