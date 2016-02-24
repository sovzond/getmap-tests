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
        private IWebDriver driver;
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
        public Boolean AssertCenter(Utils.TransformJS js1, IJavaScriptExecutor js, IWebDriver driver)
        {
            Utils.LonLat startPoint1 = js1.getMapCenter();
            double StartXL = startPoint1.getLon() - 0.03;
            double StartYL = startPoint1.getLat() - 0.03;
            double StartXR = startPoint1.getLon() + 0.03;
            double StartYR = startPoint1.getLat() + 0.03;
            GUI.MenuNavigation.get(driver).MagnifyButton();
            string left = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + StartXL + "," + StartYL + ").transform('EPSG:4326','EPSG:3857')).toString()");
            string right = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + StartXR + "," + StartYR + ").transform('EPSG:4326','EPSG:3857')).toString()");
            XY L = new XY(left);
            XY R = new XY(right);
            int XL = L.getX();
            int YL = L.getY();
            int XR = R.getX();
            int YR = R.getY();
            String[] coord = GUI.GetExtents.get(driver).GetCurrentExtent;//находим экстент после нажатия кнопки приближение обл
            Utils.LonLat bot = new Utils.LonLat(js1.transferFrom(double.Parse(coord[0]), double.Parse(coord[1]), 900913, 4326));//находим левый нижний угол
            Utils.LonLat top = new Utils.LonLat(js1.transferFrom(double.Parse(coord[2]), double.Parse(coord[3]), 900913, 4326));//находим правый верхний угол
            if (StartXL <= bot.getLon() || StartYL <= bot.getLat() || StartXR >= top.getLon() || StartYR >= top.getLat())
            {
                Assert.Fail("область не отображается");
            }
            var builder = new Actions(driver);
            IWebElement j = driver.FindElement(By.Id("map"));
            builder.MoveToElement(j, XL, YL).ClickAndHold().MoveToElement(j, XR, YR).Release().Perform();//рисуется квадрат
            Thread.Sleep(5000);
            double Lon = Math.Round(((StartXL + StartXR) / 2), 2);//находим серидину по долготе
            double Lat = Math.Round(((StartYL + StartYR) / 2), 2); //находим серидину по широте
            Utils.LonLat startPoint = js1.getMapCenter();//находим центр карты
            double lonCenter = startPoint.getLon();
            double latCenter = startPoint.getLat();
            if (Lon != lonCenter || Lat != latCenter)
            {
              return  false;
            }
            else
            {
                return true;
            }
        }
        public Boolean AssertExtent(Utils.TransformJS js1, IWebDriver driver)
        {
            String[] coord = GUI.GetExtents.get(driver).GetCurrentExtent;//находим экстент после нажатия кнопки приближение обл
            Utils.LonLat bot = new Utils.LonLat(js1.transferFrom(double.Parse(coord[0]), double.Parse(coord[1]), 900913, 4326));//находим левый нижний угол
            Utils.LonLat top = new Utils.LonLat(js1.transferFrom(double.Parse(coord[2]), double.Parse(coord[3]), 900913, 4326));//находим правый верхний угол
            GUI.ScaleMenu.get(driver).IncrementButton();
            Thread.Sleep(5000);
            String[] coordButton = GUI.GetExtents.get(driver).GetCurrentExtent; //находим экстент после нажатия кнопки приближение 
            Utils.LonLat botBut = new Utils.LonLat(js1.transferFrom(double.Parse(coordButton[0]), double.Parse(coordButton[1]), 900913, 4326));//находим левый нижний угол
            Utils.LonLat topBut = new Utils.LonLat(js1.transferFrom(double.Parse(coordButton[2]), double.Parse(coordButton[3]), 900913, 4326));//находим правый верхний угол
            if (bot.getLat() >= botBut.getLat() || bot.getLon() >= botBut.getLon() || top.getLat() <= topBut.getLat() || top.getLon() <= topBut.getLon())
            {
                return false;
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
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            Utils.TransformJS js1 = new Utils.TransformJS(driver);
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);       
             if( AssertCenter(js1, js, driver)==false)
            {
                Assert.Fail("не правильно найден центр и не отображен верный экстент карты");
            }
           if( AssertExtent( js1, driver)== false)
            {
                Assert.Fail(" экстент карты отображается");
            }
        }
    }
}
