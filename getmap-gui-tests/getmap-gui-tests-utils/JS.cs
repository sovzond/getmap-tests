using OpenQA.Selenium;
using System;
namespace GetMapTest.Utils
{
    public class TransformJS
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;

        public TransformJS(IWebDriver driver)
        {
            this.driver = driver;
            this.js = driver as IJavaScriptExecutor;
        }

        /// <summary>
        ///  Возвращает центр карты.
        /// </summary>
        /// <returns></returns>
        public LonLat GetMapCenter()
        {
            return new LonLat((string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
                                              "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
                                             "var point1 = window.portal.stdmap.map.getCenter(); " +
                                             "var point2 = point1.transform(proj900913, projWGS84); " +
                                             "return point2.toString()}"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getLon1"></param>
        /// <param name="getLat1"></param>
        /// <param name="src_cs"></param>
        /// <param name="dest_cs"></param>
        /// <returns></returns>
        public string TransferFrom(double getLon1, double getLat1, int src_cs, int dest_cs)//4326 900913
        {
            string script = string.Format("{{var projDest = new OpenLayers.Projection('EPSG:{0}');" +
                            "var projSrc = new OpenLayers.Projection('EPSG:{1}'); " +
                            "var point1 = new OpenLayers.LonLat({2},{3});" +
                            "var point2 = point1.transform(projSrc, projDest); " +
                            "return point2.toString()}}", dest_cs, src_cs, getLon1, getLat1);
            return (string)js.ExecuteScript(script);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string GetLonLatFromPixel(int x, int y)
        {
            return (string)js.ExecuteScript("return window.portal.stdmap.map.getLonLatFromPixel(new OpenLayers.Pixel( " + x + ", " + y + " )).toString()");

        }

        /// <summary>
        /// Возвращает вещественные(с плавающей точкой) координаты карты с указанными параметрами. 
        /// </summary>
        /// <param name="lonLat"></param>
        /// <returns></returns>
        public LonLat GetPixelFromLonLat(LonLat lonLat)
        {
            return (LonLat)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + lonLat.getLon() + ", " + lonLat.getLat() + ")).toString()");
        }

        /// <summary>
        /// Возвращает целочисленные координаты карты с указаннами параметрами.
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        public XY GetPixelFromPixel(XY xy)
        {
            return (XY)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + xy.getX + "," + xy.getY + ").transform('EPSG:4326','EPSG:3857')).toString()");

        }

        /// <summary>
        /// Возвращает zIndex элемента.
        /// </summary>
        /// <param name="element">Элемент, zIndex которые будет возвращен.</param>
        /// <returns></returns>
        public int zIndex(string element)
        {
            string zIndexStr = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"" + element + "\")[0].div.style.zIndex;");
            int zIndexConverted = Convert.ToInt32(zIndexStr);
            return zIndexConverted;
        }

        /// <summary>
        /// Возвращает текущий зум карты.
        /// </summary>
        /// <returns></returns>
        public int Zoom()
        {
            string zoomStr = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            int zoomConverted = Convert.ToInt32(zoomStr);
            return zoomConverted;
        }

        /// <summary>
        /// Возвращает текущий экстент карты.
        /// </summary>
        /// <returns></returns>
        public XY GetCurrentExtent()
        {
            return (XY)js.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");

        }

        /// <summary>
        /// Возвращает базовый экстент карты.
        /// </summary>
        /// <returns></returns>
        public XY GetBaseExtent()
        {
            driver.FindElement(By.CssSelector("#menuNavigation div.svzSimpleButton.fullMap")).Click();
            return GetCurrentExtent();
        }

    }
}