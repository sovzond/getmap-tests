using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace GetMapTest
{
    [TestClass]
    public class TestGoTo
    {
        private IWebElement getElementByText(IList<IWebElement> els, string text)
        {
            foreach (IWebElement el in els)
            {
                if (el.Text == text)
                {
                    return el;
                }
            }
            return null;
        }
        private void GoToCoordWnd(IWebDriver driver)
        {
            driver.FindElement(By.ClassName("gotoCoordsButton")).Click(); //делаем клик по иконке XY

            IList<IWebElement> elsTitle = driver.FindElements(By.ClassName("containerTitle"));
            //ищем текст "ПЕРЕХОД ПО КООРДИНАТАМ", при не нахождении возникает ошибка
            if (getElementByText(elsTitle, "ПЕРЕХОД ПО КООРДИНАТАМ") == null)
            {
                Assert.Fail("ПЕРЕХОД ПО КООРДИНАТАМ не найден");
            }
            GUI.InputCoordWnd.get(driver).setLon(60, 50, 50).setLat(69, 59, 0).click();//нажимаем клавишу найти
        }
        [TestMethod]
        public void GoToCoord()
        {
            IWebDriver driver = Settings.Instance.createDriver();
            Utils.TransformJS js = new Utils.TransformJS(driver);
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            Utils.LonLat startPoint = js.getMapCenter();//находим центр
            GoToCoordWnd(driver);// ищем по заданным координатам
            IList<IWebElement> img = driver.FindElements(By.ClassName("olAlphaImg"));//находим указатель
            int x = img[0].Location.X + img[0].Size.Width / 2; //ищем координаты картинки по x
            int y = img[0].Location.Y - img[0].Size.Height / 3; // ищем координаты картинки по y
            string Latimg1 = js.getLonLatFromPixel(x, y);//переводим экранные координаты
            Utils.LonLat imgCoord = new Utils.LonLat(Latimg1);// находи lon и lat кaртинки в неправильном формате
            string imgPoint = js.transferFrom(imgCoord.getLon(), imgCoord.getLat(), 900913, 4326);//находим правильный lon и lat 
            Utils.LonLat coord5 = new Utils.LonLat(imgPoint);
            double imgLon = coord5.getLon(); //находи lon кaртинки
            double imgLat = coord5.getLat();//находи lat кaртинки
            Utils.LonLat changedPoint = js.getMapCenter();  // вычисляем координаты изменившегося центра. Получаем:"lon=69.9833333333329,lat=60.84722222222229"
            if (Utils.LonLat.equalLonLat(changedPoint, startPoint)==false)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("центр не изменен");
            }
            double changedLon = changedPoint.getLon();//находим lon получившегося цента
            double changedLat = changedPoint.getLat();//находим lat получившегося цента  
            double specLon1= Utils.DegreeFormat.getDecimalDegree(69, 59, 0, 2);// находим lon введенный нами       
            double specLat1 = Utils.DegreeFormat.getDecimalDegree(60, 50, 50, 2);//находим lat введенный нами
            if (changedLon != specLon1 || changedLat != specLat1)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("не правильный переход");
            }
            // проверяем находится ли указатель в координатах заданными нами
            if (imgLon != specLon1 || imgLat != specLat1)//сравниваем начальные значения центра с изменившимися координатами заданными нами
            {
                Assert.Fail("не правильный переход");      
            }           
        }
    }
}



















