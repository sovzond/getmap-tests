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
        private int zIndex;
        private const string _fakel = "Факел";
        private const string _ambar = "Амбар";
        private const string _places = "Кустовые площадки";
        private const string _dns = "ДНС";
        private const string locationImgButtonsFakel = "//div[@dataname='wms_Факелы']/span/img";
        private const string locationImgButtonsAmbar = "//div[@dataname='wms_Амбары']/span/img";
        private const string locationImgButtonsPlaces = "//div[@dataname='wms_Кустовые площадки']/span/img";
        private const string locationImgButtonsDNS = "//div[@dataname='wms_ДНС']/span/img";
        private const string locationZindexFakel = "wms_Факелы";
        private const string locationZindexAmbar = "wms_Амбары";
        private const string locationZindexPlaces = "wms_Кустовые площадки";
        private const string locationZindexDNS = "wms_ДНС";
        private IList<IWebElement> listImgButtonsFakel;
        private IList<IWebElement> listImgButtonsAmbar;
        private IList<IWebElement> listImgButtonsPlaces;
        private IList<IWebElement> listImgButtonsDns;
        Dictionary<string, IWebElement> dicButtonsUp;
        Dictionary<string, IWebElement> dicButtonsDown;
        Utils.TransformJS js;
        private enum NumberButtonsUpDown
        {
            Up = 0,
            Dowm = 1
        }

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            js = new Utils.TransformJS(driver);
            dicButtonsUp = new Dictionary<string, IWebElement>();
            dicButtonsDown = new Dictionary<string, IWebElement>();
        }

        /// <summary>
        ///Перемещает каждый последний слой на позицию первого.
        ///</summary>
        [TestMethod]
        public void CheckIncrementLayers()
        {
            DataPreparation();  
            IncrementLayer(_dns, locationZindexDNS);
            IncrementLayer(_places, locationZindexPlaces);
            IncrementLayer(_ambar, locationZindexAmbar);
            IncrementLayer(_fakel, locationZindexFakel);
            
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void DataPreparation()
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            if (!GUI.Layers.get(driver).GetSelectedNeftyStruct)
                GUI.Layers.get(driver).NeftyStructCheckBoxClick();
            GUI.SlideMenu.get(driver).OpenLegenda();
            listImgButtonsFakel = driver.FindElements(By.XPath(locationImgButtonsFakel));
            listImgButtonsAmbar = driver.FindElements(By.XPath(locationImgButtonsAmbar));
            listImgButtonsPlaces = driver.FindElements(By.XPath(locationImgButtonsPlaces));
            listImgButtonsDns = driver.FindElements(By.XPath(locationImgButtonsDNS));
            dicButtonsUp.Add(_fakel, listImgButtonsFakel[(int)NumberButtonsUpDown.Up]);
            dicButtonsDown.Add(_fakel, listImgButtonsFakel[(int)NumberButtonsUpDown.Dowm]);
            dicButtonsUp.Add(_ambar, listImgButtonsAmbar[(int)NumberButtonsUpDown.Up]);
            dicButtonsDown.Add(_ambar, listImgButtonsAmbar[(int)NumberButtonsUpDown.Dowm]);
            dicButtonsUp.Add(_places, listImgButtonsPlaces[(int)NumberButtonsUpDown.Up]);
            dicButtonsDown.Add(_places, listImgButtonsPlaces[(int)NumberButtonsUpDown.Dowm]);
            dicButtonsUp.Add(_dns, listImgButtonsDns[(int)NumberButtonsUpDown.Up]);
            dicButtonsDown.Add(_dns, listImgButtonsDns[(int)NumberButtonsUpDown.Dowm]);
            zIndex = js.GetZIndex(locationZindexFakel);
        }

        private void IncrementLayer(string key, string locationzIndex)
        {
            for (int i = 0; i < 3; i++)
                dicButtonsUp[key].Click();
            int zIndex = js.GetZIndex(locationzIndex);
            Assert.AreEqual(this.zIndex, zIndex, "Слой не отобразился выше предыдущего");
            Thread.Sleep(1000);
        }
    }
}
