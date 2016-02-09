using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// 
    /// </summary>
    public class Layers
    {
        private IWebDriver driver;
        private IWebElement elementTestLayer;
        private IWebElement elementNewGroup;
        private IWebElement elementGasStruct;
        private IWebElement elementEnergyStruct;
        private IWebElement elementNeftyStruct;
        private IWebElement elementTematicMap;
        private IWebElement elementCosmoPhoto;
        private IList<IWebElement> listCheckBoxs;
        private const string locationCheckBoxs = "#stdportal_LayerManagerBase_1 div.dijit.dijitReset.dijitInline.dijitCheckBox input";

        private Layers(IWebDriver driver)
        {
            this.driver = driver;
        }

        private void Sleep()
        {
            Thread.Sleep(2000);
        }

        private Layers SetValueList()
        {
            this.listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private Layers SetValueLayers()
        {
            this.elementTestLayer = listCheckBoxs[0];
            this.elementNewGroup = listCheckBoxs[4];
            this.elementGasStruct = listCheckBoxs[6];
            this.elementEnergyStruct = listCheckBoxs[10];
            this.elementNeftyStruct = listCheckBoxs[16];
            this.elementTematicMap = listCheckBoxs[21];
            this.elementCosmoPhoto = listCheckBoxs[28];
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static Layers get(IWebDriver driver)
        {
            return new Layers(driver);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers TestClick()
        {
            Sleep();
            elementTestLayer.Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupClick()
        {
            Sleep();
            elementNewGroup.Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers GasStructClick()
        {
            Sleep();
            elementGasStruct.Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStruckClick()
        {
            Sleep();
            elementEnergyStruct.Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructClick()
        {
            Sleep();
            elementNeftyStruct.Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapClick()
        {
            Sleep();
            elementTematicMap.Click();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoClick()
        {
            Sleep();
            elementCosmoPhoto.Click();
            return this;
        }
           

    }
}



