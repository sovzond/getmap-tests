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

            GUI.Layers.get(driver).TestClick();
            GUI.Layers.TestLayerClass.get(driver).AmbarClick().AmericaClick().Base_RasterClick();

            GUI.Layers.get(driver).NewGroupClick();
            GUI.Layers.NewGroupClass.get(driver).Tsp_25Click();

            GUI.Layers.get(driver).GasStructClick();
            GUI.Layers.GasStructClass.get(driver).GazoprovodClick().GPZPointClick().GPZPoligonClick();

            GUI.Layers.get(driver).EnergyStruckClick();
            GUI.Layers.EnergyStructClass.get(driver).PodstationPoligon().ElectroStationPointClick().ElectroStationPoligon()
                .LEPClick().PodstationPointClick();

            GUI.Layers.get(driver).NeftyStructClick();
            GUI.Layers.NeftyStructClass.get(driver).AmbarClick().DNSClick().FakelClick().PlacesClick();

            GUI.Layers.get(driver).TematicMapClick();
            GUI.Layers.TematicMapClass.get(driver).CheckBox1().CheckBox2().CheckBox3()
                .CheckBox4().CheckBox5().CheckBox6();

            GUI.Layers.get(driver).CosmoPhotoClick();
            GUI.Layers.CosmoPhotoClass.get(driver).GazpromClick();
                    
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

    }
     
}
