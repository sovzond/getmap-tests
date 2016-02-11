using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    ///
    /// </summary>
    [TestClass]
    public class TestEnableLayer
    {
        private IWebDriver driver;
        private const string locationImageForCheck = "#OpenLayers_Layer_OSM_2 img[src*='/11/1421/586.png']";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CheckEnables()
        {
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            GUI.SlideMenu.get(driver).OpenLayers();
            CheckEnableGasStruckAndDisableGPZPoint();  
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckEnableGasStructAndEnableGPZPoint()
        {

        }
        private void CheckEnableGasStruckAndDisableGPZPoint()
        {
            GUI.Layers.get(driver).GasStructClick();
            
        }
    }
     
}
