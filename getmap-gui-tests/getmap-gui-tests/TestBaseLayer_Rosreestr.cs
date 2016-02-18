using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку на отображение тайлов из верных источников слоев 'Росреестр' и  'OpenStreetMap'.
    /// </summary>
    [TestClass]
    public class TestBaseLayer_Rosreestr
    {
        private IWebDriver driver;
        private const string locationLayersInBaseLayers = "svzLayerManagerItem";
        private IList<IWebElement> listTileA;
        private IList<IWebElement> listTileB;
        private IList<IWebElement> listTileC;
        private IList<IWebElement> listImageRosreestr;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            GUI.SlideMenu.get(driver).OpenLayers().OpenBaseLayers();
            AssertGetElementByText();
            listTileA = driver.FindElements(By.CssSelector("div.olMap img[src*='http://a.tile.openstreetmap.org']"));
            listTileB = driver.FindElements(By.CssSelector("div.olMap img[src*='http://b.tile.openstreetmap.org']"));
            listTileC = driver.FindElements(By.CssSelector("div.olMap img[src*='http://c.tile.openstreetmap.org']"));
            listImageRosreestr = driver.FindElements(By.CssSelector("div.olMap img[src*='http://maps.rosreestr.ru']"));
        }

        /// <summary>
        /// Выполняет проверку на отображение корректных тайлов из источника 'Росреестр'.
        /// </summary>
        [TestMethod]
        public void TestRosreestr()
        {
            GUI.SlideMenu.get(driver).RosreestrClick();
            List<string> ListAttributeSrc = getListAttributeSrcRos();
            for (int n = 0; n < ListAttributeSrc.Count; n++)
            {
                if (!AssertAttributeSrcRos(ListAttributeSrc[n]))
                    Assert.Fail("не показан файл из росреестра ");
            }
        }

        /// <summary>
        /// Выполняет проверку на отображение корректных тайлов из источника 'OpenStreetMap'.
        /// </summary>
        [TestMethod]
        public void TestOpenStreetMap()
        {
            GUI.SlideMenu.get(driver).OpenStreetMapClick();
            List<string> ListAttributeSrc = listAttributeSrcOpen();
            for (int n = 0; n < ListAttributeSrc.Count; n++)
            {
                if (!AssertAttributeSrcOpen(ListAttributeSrc[n]))
                    Assert.Fail("не показан файл из openSteetMap");
            }
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
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

        private bool getElementByText(IWebElement el, string text)
        {
            if (el.Text == text)
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

