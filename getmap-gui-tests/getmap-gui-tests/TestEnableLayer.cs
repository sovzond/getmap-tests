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
            CheckEnableGasStruckAndEnableGPZPoint();  
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void CheckEnableGasStruckAndEnableGPZPoint()
        {
            GUI.Layers.get(driver).GasStructClick();
            
        }
    }
     
}
