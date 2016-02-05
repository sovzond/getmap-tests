using OpenQA.Selenium;
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
        public LonLat getMapCenter()
        {
           return new LonLat((string)js.ExecuteScript("{var projWGS84 = new OpenLayers.Projection('EPSG:4326');" +
                                             "var proj900913 = new OpenLayers.Projection('EPSG:900913'); " +
                                            "var point1 = window.portal.stdmap.map.getCenter(); " +
                                            "var point2 = point1.transform(proj900913, projWGS84); " +
                                            "return point2.toString()}"));       
        }
        public string transferFrom(double getLon1, double getLat1, int src_cs, int dest_cs)//4326 900913
        {
            string script = string.Format("{{var projDest = new OpenLayers.Projection('EPSG:{0}');" +
                            "var projSrc = new OpenLayers.Projection('EPSG:{1}'); " +
                            "var point1 = new OpenLayers.LonLat({2},{3});" +
                            "var point2 = point1.transform(projSrc, projDest); " +
                            "return point2.toString()}}", dest_cs, src_cs, getLon1, getLat1);
            return (string)js.ExecuteScript(script);
        }

        public string getLonLatFromPixel(int x, int y)
        {
            return  (string)js.ExecuteScript("return window.portal.stdmap.map.getLonLatFromPixel(new OpenLayers.Pixel( " + x + ", " + y + " )).toString()");//переводим экранные координаты
        }
    }
}

