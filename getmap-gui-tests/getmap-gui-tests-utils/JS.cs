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
        ///  Возвращает центр карты типа LonLat
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
        /// Возвращает центра карты типа string
        /// </summary>
        /// <returns></returns>
        public string GetMapCenterString()
        {
            string lonlatCenter = (string)js.ExecuteScript("return window.portal.stdmap.map.getCenter().toString()");
            return lonlatCenter;
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
        /// Возвращает lon lat типа string из пикселей.
        /// </summary>
        /// <param name="x">Пиксели по оси X</param>
        /// <param name="y">Пиксели по оси Y</param>
        /// <returns></returns>
        public string GetLonLatFromPixel(int x, int y)
        {
            return (string)js.ExecuteScript("return window.portal.stdmap.map.getLonLatFromPixel(new OpenLayers.Pixel( " + x + ", " + y + " )).toString()");
        }
        
        /// <summary>
        /// Возвращает вещественные(с плавающей точкой) координаты карты с указанными параметрами типа LonLat
        /// </summary>
        /// <param name="lonLat"></param>
        /// <returns></returns>
        public LonLat GetPixelsFromLonLat(LonLat lonLat)
        {
            return new LonLat((string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + lonLat.getLon() + ", " + lonLat.getLat() + ")).toString()"));
        }

        /// <summary>
        /// Возвращает пиксели типа string из lon lat.
        /// </summary>
        /// <param name="lon">Широта</param>
        /// <param name="lat">Долгота</param>
        /// <returns></returns>
        public string GetPixelsFromLonLat(string lon,string lat)
        {
            string pixels = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + lon + "," + lat + ")).toString()");
            return pixels;
        }

        /// <summary>
        /// Возвращает пиксели типа string из lon lat.
        /// </summary>
        /// <param name="lonlat">Строка включающая в себя не только цифры</param>
        /// <returns></returns>
        public string GetPixelsFromLonLat(string lonlat)
        {
            string[] splitedLonLat = lonlat.Split('=', ',');
            string pixels = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + splitedLonLat[1] + "," + splitedLonLat[3] + ")).toString()");
            return pixels;
        }

        /// <summary>
        /// Возвращает zIndex элемента.
        /// </summary>
        /// <param name="element">Элемент, zIndex которые будет возвращен.</param>
        /// <returns></returns>
        public int GetZIndex(string element)
        {
            string zIndexStr = (string)js.ExecuteScript("return window.portal.stdmap.map.getLayersByName(\"" + element + "\")[0].div.style.zIndex;");
            int zIndexConverted = Convert.ToInt32(zIndexStr);
            return zIndexConverted;
        }

        /// <summary>
        /// Возвращает текущий зум карты.
        /// </summary>
        /// <returns></returns>
        public int GetZoom()
        {
            string zoomStr = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            int zoomConverted = Convert.ToInt32(zoomStr);
            return zoomConverted;
        }

        /// <summary>
        /// Возвращает текущий экстент карты.
        /// </summary>
        /// <returns></returns>
        public string[] GetCurrentExtentSplited()
        {
           
            string[] splited = GetCurrentExtent().Split(',');
            return splited;
        }

        /// <summary>
        /// Возвращает текущий экстент карты типа string
        /// </summary>
        /// <returns></returns>
        public string GetCurrentExtent()
        {
            string fulllink = (string)js.ExecuteScript("return window.portal.stdmap.map.getExtent().toString()");
            return fulllink;
        }

        /// <summary>
        /// Возвращает базовый экстент карты типа string[].
        /// </summary>
        /// <returns></returns>
        public string[] GetBaseExtentSplited()
        {
            driver.FindElement(By.CssSelector("#menuNavigation div.svzSimpleButton.fullMap")).Click();
            return GetCurrentExtentSplited();
        }

        /// <summary>
        /// Извлекает из ссылки экстент и возвращает его ввиде массива строк.
        /// </summary>
        /// <param name="fulllink">Ссылка, из которой необходимо извлечь экстент</param>
        /// <returns></returns>
        public string[] SplitExtentFromLink(string fulllink)
        {
            int idx = fulllink.IndexOf('=');
            idx++;
            string onlyExtent = fulllink.Substring(idx);
            string[] splited = onlyExtent.Split(',');
            return splited;
        }

        /// <summary>
        /// Возвращает целочисленный массив пикселей.
        /// </summary>
        /// <param name="pixels">Строка пикселей, которую необходимо распелить на массив.</param>
        /// <returns></returns>
        public  int[] SplitPixels(string pixels)
        {
            int[] array = new int[2];
            string[] splited = pixels.Split('=', ',', 'y', 'x');
            array[0] = Convert.ToInt32(splited[2]);
            array[1] = Convert.ToInt32(splited[5]);
            return array;
        }

        /// <summary>
        /// Переводит значение из lon lat в пиксели, возвращает в качестве целочисленного массива.
        /// </summary>
        /// <param name="lonlat">Строка включающая в себя не только цифры</param>
        /// <returns></returns>
        public int[] ConvertToSplitedPixelsFromLonLat(string lonlat)
        {
            string[] splitedLonLat = lonlat.Split('=', ',');
            string pixels = (string)js.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(" + splitedLonLat[1] + "," + splitedLonLat[3] + ")).toString()");
            return SplitPixels(pixels);
        }
    }
}