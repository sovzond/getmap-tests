using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using OpenQA.Selenium;
using GetMapTest.Utils;

namespace GetMapTest
{
    class TestCheckCoordsView
    {
        private IWebDriver driver;

        [TestMethod]
        public void TestMeth()
        {
            driver = Settings.Instance.createDriver();

            TransformJS d = new TransformJS(driver);//переименовать d

            driver.Navigate().GoToUrl("http://192.168.11.150/sovzond/portal/login.aspx");
            driver.FindElement(By.Id("txtUser")).SendKeys("guest");
            driver.FindElement(By.Id("txtPsw")).Clear();
            driver.FindElement(By.Id("txtPsw")).SendKeys("guest");
            driver.FindElement(By.Id("cmdLogin")).Click();


            LonLat center = d.getMapCenter(CoordType.meters);

            LonLat scrCenter = d.getPixelFromLonLat(center);




            var erty = new OpenQA.Selenium.Interactions.Actions(driver);
            erty.MoveByOffset((int)scrCenter.getLat(), (int)scrCenter.getLon()).Perform();

          


            System.Threading.Thread.Sleep(3000);
            IWebElement elmCoord = driver.FindElement(By.Id("dCoord"));
            System.Drawing.Point loc = elmCoord.Location;

            var builder = new OpenQA.Selenium.Interactions.Actions(driver);
            builder.MoveByOffset(loc.X + 5, loc.Y).Perform();

            System.Threading.Thread.Sleep(3000);
            IList<IWebElement> l = driver.FindElements(By.CssSelector("#dCoord p"));

            foreach (IWebElement desGrad in l)
            {
                if (desGrad.Text.CompareTo("Десятичные градусы") == 0)
                {
                    desGrad.Click();
                    break;

                }
            }

        }
    }
}