using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GetMapTest
{
    /// <summary>
    /// Выполняет проверку на то, что отображаемая область карты изменена, а в точке указанных координат установлен указатель.
    /// </summary>
    [TestClass]
    public class TestGoTo
    {
        private IWebDriver driver;
        private const string locationTextInBoxShadow = "div.containerTitle";
        private const string locationPointer = ".olAlphaImg";

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
        }

        /// <summary>
        /// Выполняет проверку на то, что система спозиционировала окно карты таким образом,
        /// что точка с заданными координатами перемещена в центр экрана. 
        /// В точке с указанными координатами установлен указатель.
        /// </summary>
        [TestMethod]
        public void GoToCoord()
        {
            Utils.TransformJS js = new Utils.TransformJS(driver);
            Utils.LonLat startPoint = js.GetMapCenter();
            GoToCoordWnd(driver);
            IList<IWebElement> img = driver.FindElements(By.CssSelector(locationPointer));
            int x = img[0].Location.X + img[0].Size.Width / 2;
            int y = img[0].Location.Y - img[0].Size.Height / 3;
            string Latimg1 = js.GetLonLatFromPixel(x, y);
            Utils.LonLat imgCoord = new Utils.LonLat(Latimg1);
            string imgPoint = js.TransferFrom(imgCoord.getLon(), imgCoord.getLat(), 900913, 4326);
            Utils.LonLat coord5 = new Utils.LonLat(imgPoint);
            double imgLon = coord5.getLon();
            double imgLat = coord5.getLat();
            Utils.LonLat changedPoint = js.GetMapCenter();
            if (!Utils.LonLat.equalLonLat(changedPoint, startPoint))
                Assert.Fail("центр не изменен");
            double changedLon = changedPoint.getLon();
            double changedLat = changedPoint.getLat();
            double specLon1 = Utils.DegreeFormat.getDecimalDegree(69, 59, 0, 2);
            double specLat1 = Utils.DegreeFormat.getDecimalDegree(60, 50, 50, 2);
            if (changedLon != specLon1 || changedLat != specLat1)
                Assert.Fail("не правильный переход");
            if (imgLon != specLon1 || imgLat != specLat1)
                Assert.Fail("не правильный переход");
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private bool IsEnableBoxShadow(IList<IWebElement> els)
        {
            foreach (IWebElement el in els)
            {
                if (el.Text == "ПЕРЕХОД ПО КООРДИНАТАМ")
                    return true;
            }
            return false;
        }

        private void GoToCoordWnd(IWebDriver driver)
        {
            GUI.MenuNavigation.get(driver).GotoCoordsButton();
            IList<IWebElement> listTitle = driver.FindElements(By.CssSelector(locationTextInBoxShadow));
            if (!IsEnableBoxShadow(listTitle))
                Assert.Fail("ПЕРЕХОД ПО КООРДИНАТАМ не найден");
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(69, 59, 0).click();
        }
    }
}



















