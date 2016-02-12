using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
namespace GetMapTest
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
      
        public void MouseMoveByOffset(int offsetX, int offsetY)
        {
           
            var builder = new Actions(driver);
            builder.MoveByOffset(offsetX, offsetY).Perform();
        }
        public void MouseMoveToElement(IWebElement element, int offsetX, int offsetY)
        {
            
            var builder = new Actions(driver);
            builder.MoveToElement(element, offsetX, offsetY).Perform();
        }
        
        [TestMethod]
        public void TestMet1()
        {
            
            IWebDriver driver = Settings.Instance.createDriver();
            var builder = new Actions(driver);
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_13")).Click();
            string left = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(70,60.88).transform('EPSG:4326','EPSG:3857')).toString()");
            string right = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(70.024,60.895).transform('EPSG:4326','EPSG:3857')).toString()");
            String[] arrL = left.Split(new Char[] { ',', '=' });
            String[] arrR = right.Split(new Char[] { ',', '=' });          
            int XL = Int32.Parse(arrL[1]);
            int YL = Int32.Parse(arrL[3]);
            int XR = Int32.Parse(arrR[1]);
            int YR = Int32.Parse(arrR[3]);
            IWebElement j = driver.FindElement(By.Id("map"));
            builder.MoveToElement(j, XL, YL).ClickAndHold().Perform();
            Thread.Sleep(5000);
            builder.MoveToElement(j, XR, YR).Release().Perform();
            Thread.Sleep(5000);
            /* builder.MoveToElement(j);
             Thread.Sleep(5000);
             builder.MoveByOffset(XL, YL).ClickAndHold();
             builder.MoveByOffset(XR, YR).Release();
             Thread.Sleep(5000);*/

        }

    }
}
