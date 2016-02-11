using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
namespace GetMapTest
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        public void MouseMoveByOffset(int offsetX, int offsetY)
        {
           
            var builder = new OpenQA.Selenium.Interactions.Actions(driver);
            builder.MoveByOffset(offsetX, offsetY).Perform();
        }
        public void MouseMoveToElement(IWebElement element, int offsetX, int offsetY)
        {
            
            var builder = new OpenQA.Selenium.Interactions.Actions(driver);
            builder.MoveToElement(element, offsetX, offsetY).Perform();
        }
        
        [TestMethod]
        public void TestMet()
        {
            IWebDriver driver = Settings.Instance.createDriver();         
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();
            Thread.Sleep(4000);
            var builder = new Actions(driver);
            IList<IWebElement> el1 = driver.FindElements(By.Id("svzLayerManagerText"));
            IWebElement el = el1[12];
            System.Drawing.Point loc = el.Location;
            MouseMoveToElement(el, loc.X, loc.Y);
            MouseMoveByOffset(loc.X, loc.Y);
        }

    }
}
