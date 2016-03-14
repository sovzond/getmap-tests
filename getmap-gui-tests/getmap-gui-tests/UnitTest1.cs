using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
namespace GetMapTest
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        Utils.TransformJS jsTransform;
        private IJavaScriptExecutor jsExecutor;
        public class XY
        {
            private int y;
            private int x;

            public XY(String arrg)
            {
                String[] arr = arrg.Split(new Char[] { ',', '=' });

                this.y = Int32.Parse(arr[3]);
                this.x = Int32.Parse(arr[1]);
            }

            public XY(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int getX
            {
                get
                {
                    return x;
                }

            }
            public int getY
            {
                get
                {
                    return y;
                }

            }
        }
        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            jsExecutor = driver as IJavaScriptExecutor;
            jsTransform = new Utils.TransformJS(driver);
        }

        [TestMethod]
        public void TestMet1()
        {
            Utils.TransformJS jsTransform = new Utils.TransformJS(driver);
            Utils.LonLat startPoint1 = jsTransform.GetMapCenter();
            double StartXL = startPoint1.getLon() + 0.004;
            double StartYL = startPoint1.getLat() + 0.004;
            double StartXR = startPoint1.getLon();
            double StartYR = startPoint1.getLat();
            string left = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + StartXL + "," + StartYL + ").transform('EPSG:4326','EPSG:3857')).toString()");
            string right = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + StartXR + "," + StartYR + ").transform('EPSG:4326','EPSG:3857')).toString()");
            XY L = new XY(left);
            XY R = new XY(right);
            int XL = L.getX;
            int YL = L.getY;
            int XR = R.getX;
            int YR = R.getY;
            string Latimg = jsTransform.GetLonLatFromPixel(XL, YL);
            string Latimg1 = jsTransform.GetLonLatFromPixel(XR, YR);
            Utils.LonLat Coord = new Utils.LonLat(Latimg);
            Utils.LonLat Coord1 = new Utils.LonLat(Latimg1);
            var builder = new Actions(driver);
            IWebElement map = driver.FindElement(By.CssSelector("#map"));
            GUI.MenuNavigation.get(driver).RuleButton();
            builder.MoveToElement(map, XL, YL).Click().MoveToElement(map, XR, YR).Click().Perform();
            Thread.Sleep(5000);
            double t= Math.Round( Math.Sqrt(Math.Pow((Coord.getLat() - Coord1.getLat()),2) + Math.Pow((Coord.getLon() - Coord1.getLon()), 2))/1774.4,1);
            IList< IWebElement> g = driver.FindElements(By.ClassName("container"));
            string h = "";
            for (int i = 0; i < g.Count; i++)
                if (g[i].Text != "") { h = g[i].Text; }                                          
            String[] arr = h.Split(new Char[]{ ':',' '});
            double u1 = Math.Round(Convert.ToDouble(arr[9].Replace(',','.')),1);
            if (t!=u1)
            {
                Assert.Fail("длина найдена неправильно");
            }
            GUI.ScaleMenu.get(driver).IncrementButton();    
        }
        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }
    }
}
