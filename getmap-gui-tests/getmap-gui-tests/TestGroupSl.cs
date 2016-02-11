using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace GetMapTest
{
    [TestClass]
    public class UnitTest2
    {
        private IWebDriver driver;

        private void MouseMoveByOffset(int offsetX, int offsetY)
        {
            var builder = new Actions(driver);
            builder.MoveByOffset(offsetX, offsetY).Perform();
        }
        public void MouseMoveToElement(IWebElement element, int offsetX, int offsetY)
        {
          
            var builder = new Actions(driver);
            builder.MoveToElement(element, offsetX, offsetY).Perform();
        }
        [TestMethod]
        public void TestMet()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();
            IList<IWebElement> elm_coord = driver.FindElements(By.ClassName("svzLayerManagerText"));
            Thread.Sleep(4000);
            IWebElement elm_coord1 = elm_coord[12];
            System.Drawing.Point loc = elm_coord1.Location;
            MouseMoveToElement(elm_coord1, loc.X, loc.Y);
            MouseMoveByOffset(loc.X, loc.Y);
            Thread.Sleep(4000);

        }
    }
}
