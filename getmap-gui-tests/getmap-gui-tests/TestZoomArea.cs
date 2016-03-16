using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
namespace GetMapTest
{
    /// <summary>
    ///  Выполняет проверку инструмента 'Приблежение области',а так же работает с увелечением экстента карты.
    /// </summary>
    [TestClass]
    public class TestZoomArea
    {
        private IWebDriver driver;
        private IJavaScriptExecutor jsExecutor;
        Utils.TransformJS jsTransform;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            jsExecutor = driver as IJavaScriptExecutor;
            jsTransform = new Utils.TransformJS(driver);
        }

        /// <summary>
        /// Проверяет действительно ли:
        /// указанный экстент карты целиком отображен на экране;
        ///  центр выбранного экстента совпадает с центром карты. 
        /// </summary>
        [TestMethod]
        public void TestZoom()
        {
            Assert.IsTrue(AssertCenter(jsTransform, jsExecutor, driver), "не правильно найден центр и не отображен верный экстент карты");
            Assert.IsFalse(AssertExtent(jsTransform, driver), "экстент карты отображается");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private Boolean AssertExtentMap(double XLeftH, double YbotH, double XRightH, double YTopH, double XLeftB, double YbotB, double XRightB, double YTopB)
        {
            if (XLeftH <= YTopB || YbotH <= YbotB || XRightH >= XRightB || YTopH >= YTopB)
                return false;
            return true;
        }

        /// <summary>
        ///  находит X и Y
        /// </summary>
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

        /// <summary>
        /// проверяет правильно ли отцентрирована карта после выделения области
        /// также проверяет входит ли заданная область на экстент карты
        /// </summary>
        public Boolean AssertCenter(Utils.TransformJS jsTransform, IJavaScriptExecutor jsExecutor, IWebDriver driver)
        {
            Utils.LonLat startPoint1 = jsTransform.GetMapCenter();
            double StartXL = startPoint1.getLon() - 0.03;
            double StartYL = startPoint1.getLat() - 0.03;
            double StartXR = startPoint1.getLon() + 0.03;
            double StartYR = startPoint1.getLat() + 0.03;
            GUI.MenuNavigation.get(driver).MagnifyButton();
            string left = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + StartXL + "," + StartYL + ").transform('EPSG:4326','EPSG:3857')).toString()");
            string right = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + StartXR + "," + StartYR + ").transform('EPSG:4326','EPSG:3857')).toString()");
            Utils.XY L = new Utils.XY(left);
            Utils.XY R = new Utils.XY(right);
            int XL = L.getX;
            int YL = L.getY;
            int XR = R.getX;
            int YR = R.getY;
            String[] coord = GetExtent();
            Utils.LonLat bot = new Utils.LonLat(jsTransform.TransferFrom(double.Parse(coord[0]), double.Parse(coord[1]), 900913, 4326));
            Utils.LonLat top = new Utils.LonLat(jsTransform.TransferFrom(double.Parse(coord[2]), double.Parse(coord[3]), 900913, 4326));
            Assert.IsFalse(AssertExtentMap(StartXL, StartYL, StartXR, StartYR, bot.getLon(), bot.getLat(), top.getLon(), top.getLat()), "область не отображается");
            var builder = new Actions(driver);
            IWebElement map = driver.FindElement(By.CssSelector("#map"));
            builder.MoveToElement(map, XL, YL).ClickAndHold().MoveToElement(map, XR, YR).Release().Perform();
            Thread.Sleep(5000);
            double Lon = Math.Round(((StartXL + StartXR) / 2), 2);
            double Lat = Math.Round(((StartYL + StartYR) / 2), 2);
            Utils.LonLat startPoint = jsTransform.GetMapCenter();
            double lonCenter = startPoint.getLon();
            double latCenter = startPoint.getLat();
            if (Lon != lonCenter || Lat != latCenter)
                return false;
            return true;
        }

        /// <summary>
        /// проверяет находится ли экстент заданной до этого области
        /// после нажатия кнопки приближение
        /// </summary>
        public Boolean AssertExtent(Utils.TransformJS jsTransform, IWebDriver driver)
        {
            Utils.LonLat startPoint1 = jsTransform.GetMapCenter();
            double StartXL = startPoint1.getLon() - 0.03;
            double StartYL = startPoint1.getLat() - 0.03;
            double StartXR = startPoint1.getLon() + 0.03;
            double StartYR = startPoint1.getLat() + 0.03;
            GUI.ScaleMenu.get(driver).IncrementButton();
            Thread.Sleep(5000);
            String[] coordButton = GetExtent();
            Utils.LonLat botBut = new Utils.LonLat(jsTransform.TransferFrom(double.Parse(coordButton[0]), double.Parse(coordButton[1]), 900913, 4326));
            Utils.LonLat topBut = new Utils.LonLat(jsTransform.TransferFrom(double.Parse(coordButton[2]), double.Parse(coordButton[3]), 900913, 4326));
            if (AssertExtentMap(StartXL, StartYL, StartXR, StartYR, topBut.getLon(), topBut.getLat(), botBut.getLon(), botBut.getLat()))
                return true;
            return false;
        }

        private string[] GetExtent()
        {
            string fulllink = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");
            string[] splited = fulllink.Split(',');
            return splited;
        }
    }
}
