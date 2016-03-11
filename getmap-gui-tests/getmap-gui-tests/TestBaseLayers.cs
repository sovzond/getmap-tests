using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;


namespace GetMapTest
{
    /// <summary>
    ///Выполняет проверку на отображение слоев гугл, а так же на автоматическое включение  слоя OpenStreetMap  
    /// </summary>
    [TestClass]
    public class TestBaseLayers
    {
        private IWebDriver driver;
        private const string urlGoogleImage = "http://maps.googleapis.com/maps/api/js/StaticMapService.GetMapImage";
        private const string locationBaseLayersChildContainer = "layerManagerBasemap";
        private const string locationLayersInBaseLayers = "svzLayerManagerItem";
        private const string locationRadioButtonOSM = "#layerManagerBasemap div.dijit.dijitReset.dijitInline.dijitRadio.dijitRadioChecked.dijitChecked input";
        private IList<IWebElement> listGoogleImage;
        private IList<IWebElement> listTileA;
        private IList<IWebElement> listTileB;
        private IList<IWebElement> listTileC;
        private IList<IWebElement> listImageRosreestr;
        private List<string> srcGoogleImage;
        private enum NumberLayers
        {
            Google = 0,
            Scheme = 1,
            Sputnik = 2,
            Gibrid = 3,
            Rosreestr = 4,
            OSM = 5
        }

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            listGoogleImage = null;
            srcGoogleImage = new List<string>();
            listTileA = driver.FindElements(By.CssSelector("div.olMap img[src*='http://a.tile.openstreetmap.org']"));
            listTileB = driver.FindElements(By.CssSelector("div.olMap img[src*='http://b.tile.openstreetmap.org']"));
            listTileC = driver.FindElements(By.CssSelector("div.olMap img[src*='http://c.tile.openstreetmap.org']"));
            listImageRosreestr = driver.FindElements(By.CssSelector("div.olMap img[src*='http://maps.rosreestr.ru']"));
        }

        /// <summary>
        ///Выполняет проверку на отображение слоев гугл, а так же на автоматическое включение  слоя OpenStreetMap
        ///</summary>
        [TestMethod]
        public void CheckBaseLayers()
        {
            OpenSlideMenu();
            CheckSelectedOSM();
            AssertGetElementByText();
            CheckLayerScheme();
            CheckLayerSputnik();
            CheckLayerGibrid();
        }

        /// <summary>
        /// Выполняет проверку на отображение корректных тайлов из источника 'OpenStreetMap'.
        /// </summary>
        [TestMethod]
        public void TestOpenStreetMap()
        {
            OpenSlideMenu();
            GUI.SlideMenu.get(driver).OpenStreetMapClick();
            List<string> ListAttributeSrc = listAttributeSrcOpen();
            for (int n = 0; n < ListAttributeSrc.Count; n++)
            {
                if (!AssertAttributeSrcOpen(ListAttributeSrc[n]))
                    Assert.Fail("не показан файл из openSteetMap");
            }
        }

        /// <summary>
        /// Выполняет проверку на отображение корректных тайлов из источника 'Росреестр'.
        /// </summary>
        [TestMethod]
        public void TestRosreestr()
        {
            OpenSlideMenu();
            GUI.SlideMenu.get(driver).RosreestrClick();
            List<string> ListAttributeSrc = getListAttributeSrcRos();
            for (int n = 0; n < ListAttributeSrc.Count; n++)
            {
                if (!AssertAttributeSrcRos(ListAttributeSrc[n]))
                    Assert.Fail("не показан файл из росреестра ");
            }
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void OpenSlideMenu()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(200);
            GUI.SlideMenu.get(driver).OpenBaseLayers().OpenGoogle();
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
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[(int)NumberLayers.Google], "Google"), "Не найден Google.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[(int)NumberLayers.Scheme], "Схема"), "Не найдет Схема.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[(int)NumberLayers.Sputnik], "Спутник"), "Не найден Спутник.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[(int)NumberLayers.Gibrid], "Гибрид"), "Не найден гибрид.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[(int)NumberLayers.Rosreestr], "Росреестр"), "Не найден Росреестр.");
                Assert.IsFalse(!getElementByText(listLayersInBaseLayers[(int)NumberLayers.OSM], "OpenStreetMap"), "Не найден OpenStreetMap.");
            }
        }

        private List<string> AddInListAttributeSrc(IList<IWebElement> elementsForAdd)
        {
            List<string> listAttributeSrc = new List<string>();
            foreach (var el in elementsForAdd)
                listAttributeSrc.Add(el.GetAttribute("src"));
            return listAttributeSrc;
        }

        private List<string> listAttributeSrcOpen()
        {
            List<string> listAttributeSrc = new List<string>();

            listAttributeSrc = AddInListAttributeSrc(listTileC);
            listAttributeSrc = AddInListAttributeSrc(listTileA);
            listAttributeSrc = AddInListAttributeSrc(listTileB);
            return listAttributeSrc;
        }

        private bool AssertAttributeSrcOpen(string listAttributeSrc)
        {
            if (listAttributeSrc.StartsWith("http://c.tile.openstreetmap.org")
                || listAttributeSrc.StartsWith("http://a.tile.openstreetmap.org")
                || listAttributeSrc.StartsWith("http://b.tile.openstreetmap.org"))
                return true;
            return false;
        }

        private bool AssertAttributeSrcRos(string ListAttributeSrc)
        {
            if (ListAttributeSrc.StartsWith("http://maps.rosreestr.ru/"))
                return true;
            return false;
        }

        private List<string> getListAttributeSrcRos()
        {
            List<string> listAttributeSrc = new List<string>();
            foreach (var el in listImageRosreestr)
                listAttributeSrc.Add(el.GetAttribute("src"));
            return listAttributeSrc;
        }
    }
}
