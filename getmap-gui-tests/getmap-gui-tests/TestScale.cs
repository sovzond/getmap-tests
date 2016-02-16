using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GetMapTest
{
    [TestClass]
    public class TestScale
    {
        private IWebDriver driver;
        [TestInitialize]
        public void SetupTest()
        {
            driver = Settings.Instance.createDriver();
        }
         [TestMethod]
        public void TestMasshtabPlus()                                                                               
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Thread.Sleep(2000);           
            string Zamer = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertZamer = Convert.ToInt64(Zamer);                                                                  
            GUI.ScaleMenu.get(driver).IncrementButton();                                    
            Thread.Sleep(2000);
            string Izmer = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertIzmer = Convert.ToInt64(Izmer);                                                                
            if (convertZamer >= convertIzmer)                                                                           
                Assert.Fail("после клика ");
            double zoom2 = Convert.ToDouble(convertIzmer);
            if (zoom2 - 1 != convertZamer)
                Assert.Fail("уровень Zoom'а не увеличился на еденицу");                                                         
            

        }
        [TestMethod]
        public void TestMasshtabMinus()                                                                              
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Thread.Sleep(2000);
            string Zamer = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertZamer = Convert.ToInt64(Zamer);                                                                  
            GUI.ScaleMenu.get(driver).DecrementButton();                                                        
            Thread.Sleep(2000);
            string Izmer = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertIzmer = Convert.ToInt64(Izmer);                                                                
            if (convertZamer <= convertIzmer)                                                                           
                Assert.Fail("после клика ");
            double zoom2 = Convert.ToDouble(convertIzmer);
            if (zoom2 + 1 != convertZamer)
                Assert.Fail("уровень Zoom'а не уменьшился на еденицу");                                                         


        }
        [TestCleanup]
        public void Clean()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}
