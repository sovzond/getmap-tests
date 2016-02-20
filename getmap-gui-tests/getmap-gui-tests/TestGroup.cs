using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
namespace GetMapTest
{
    [TestClass]
    public class UnitTest1
    {
       private IJavaScriptExecutor js;
        private Utils.TransformJS js1;
        public class XY
        {
            private int Y;
            private int X;       
            public  XY(String arrg)
            {
                String[] arr = arrg.Split(new Char[] { ',', '=' });

                this.Y = Int32.Parse(arr[3]);
                this.X = Int32.Parse(arr[1]);                 
            }
            public XY(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
            public  int getX()
            {
                return X;
            }
            public int getY()
            {
                return Y;
            }     
        }
            public string[] getExtent(IJavaScriptExecutor js)
        {
            
            string obl = (string)js.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");
            return obl.Split(new Char[] { ',' });
        }
        public Boolean AssertCenter(IJavaScriptExecutor js)
        {
            double k = Math.Round(((70 + 70.024) / 2), 2);//находим серидину по долготе
            double k1 = Math.Round(((60.88 + 60.895) / 2), 2); //находим серидину по широте
            Utils.LonLat startPoint = js1.getMapCenter();//находим центр карты
            double lon = startPoint.getLon();
            double lat = startPoint.getLat();
            if (k != lon || k1 != lat)
            {
              return  false;
            }
            else
            {
                return true;
            }
        }
    [TestMethod]
        public void TestMet1()
        {      
            IWebDriver driver = Settings.Instance.createDriver();
            var builder = new Actions(driver);            
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            Utils.TransformJS js1 = new Utils.TransformJS(driver);
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            GUI.MenuNavigation.get(driver).MagnifyButton();
            string left = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(70,60.88).transform('EPSG:4326','EPSG:3857')).toString()");
            string right = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(70.024,60.895).transform('EPSG:4326','EPSG:3857')).toString()");
            XY L = new XY(left);
            XY R= new XY(right);                
            int XL = L.getX();
            int YL = L.getY();
            int XR = R.getX(); ;
            int YR = R.getY(); ;
            IWebElement j = driver.FindElement(By.Id("map"));
            builder.MoveToElement(j, XL, YL).ClickAndHold().MoveToElement(j, XR, YR).Release().Perform();//рисуется квадрат
            Thread.Sleep(5000);
            double k= Math.Round(((70+ 70.024)/2),2);//находим серидину по долготе
            double k1 = Math.Round(((60.88 + 60.895) / 2),2); //находим серидину по широте
            Utils.LonLat startPoint = js1.getMapCenter();//находим центр карты
            double lon = startPoint.getLon();
            double lat = startPoint.getLat();
            if (k != lon || k1!= lat)
            {
                Assert.Fail("не правильный переход");
            }
            String[] coord = getExtent(js);//находим экстент карты
            Utils.LonLat lo = new Utils.LonLat(js1.transferFrom(double.Parse(coord[0]), double.Parse(coord[1]), 900913, 4326));//находим левый нижний угол
            Utils.LonLat lo1 = new Utils.LonLat(js1.transferFrom(double.Parse(coord[2]), double.Parse(coord[3]), 900913, 4326));//находим правый верхний угол


        }

    }
}
