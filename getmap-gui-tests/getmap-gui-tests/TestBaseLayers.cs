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
        private List<string> srcGoogleImage;
        private const string urlGoogleImage = "http://maps.googleapis.com/maps/api/js/StaticMapService.GetMapImage";
        private const string locationBaseLayersChildContainer = "layerManagerBasemap";
        private const string locationLayersInBaseLayers = "svzLayerManagerItem";
        private const string locationRadioButtonOSM = "#layerManagerBasemap div.dijit.dijitReset.dijitInline.dijitRadio.dijitRadioChecked.dijitChecked input";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            listGoogleImage = null;
            srcGoogleImage = new List<string>();
        }

        /// <summary>
        ///Выполняет проверку на отображение слоев гугл, а так же на автоматическое включение  слоя OpenStreetMap
        ///</summary>
        [TestMethod]
        public void CheckBaseLayers()
        {
            GUI.SlideMenu.get(driver).OpenLayers().OpenBaseLayers();
            CheckSelectedOSM();
            AssertGetElementByText();
            GUI.SlideMenu.get(driver).OpenGoogle();
            CheckLayerScheme();
            CheckLayerSputnik();
            CheckLayerGibrid();
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
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
            CheckUrlLayers("Схема");
        }

        private void CheckLayerSputnik()
        {
            GUI.SlideMenu.get(driver).LayerSputnikClick();
            CheckUrlLayers("Спутник");
        }

        private void CheckLayerGibrid()
        {
            GUI.SlideMenu.get(driver).LayerGibridClick();
            CheckUrlLayers("Гибрид");
        }

        private void CheckUrlLayers(string layer)
        {
            listGoogleImage = driver.FindElements(By.CssSelector("div.gm-style img[src*='google']"));
            List<string> src = new List<string>();
            foreach (var r in listGoogleImage)
                src.Add(r.GetAttribute("src"));
            if (!src[0].StartsWith(urlGoogleImage))
                Assert.Fail("Слой" + layer + "имеет не верный путь изображения.");
        }

        private bool getElementByText(IWebElement el, string text)
        {
            if (el.Text == text)
                return true;
            return false;
        }

        private void AssertGetElementByText()
        {
            IList<IWebElement> listLayersInBaseLayers = driver.FindElements(By.ClassName(locationLayersInBaseLayers));
            if (listLayersInBaseLayers != null)
            {
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[0], "Google"), "Не найден Google.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[4], "Росреестр"), "Не найден Росреестр.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[5], "OpenStreetMap"), "Не найден OpenStreetMap.");
            }
        }
    }
}
