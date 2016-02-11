using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GetMapTest
{
    [TestClass]
    public class TestScale
    {
        private IWebDriver driver;
        private const string baseURL = "http://91.143.44.249/sovzond/portal/login.aspx?ReturnUrl=%2fsovzond%2fportal";
        [TestInitialize]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
        }
         [TestMethod]
        public void TestMasshtabPlus()                                                                               //тест на увеличение масштаба
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("txtUser")).SendKeys("student");                                                //логин
            driver.FindElement(By.Id("txtPsw")).SendKeys("123");                                                     //пароль
            driver.FindElement(By.Id("cmdLogin")).Click();                                                           //вход
            Thread.Sleep(2000);           
            string Z = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertZ = Convert.ToInt64(Z);                                                                  //снятие данных масштаба 1
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_7")).Click();                                      //"увеличить масштаб"
            Thread.Sleep(2000);
            string Z1 = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertZ1 = Convert.ToInt64(Z1);                                                                //снятие данных масштаба 2
            if (convertZ >= convertZ1)                                                                           //проверка
                Assert.Fail("после клика ");
            double zoom2 = Convert.ToDouble(convertZ1);
            if (zoom2 - 1 != convertZ)
                Assert.Fail("уровень Zoom'а не увеличился на еденицу");                                                         //сообщение о неудачнй проверке


        }
        [TestMethod]
        public void TestMasshtabMinus()                                                                              //тест на уменьшение масштаба
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.Id("txtUser")).SendKeys("student");                                                //логин
            driver.FindElement(By.Id("txtPsw")).SendKeys("123");                                                     //пароль
            driver.FindElement(By.Id("cmdLogin")).Click();                                                           //вход
            Thread.Sleep(2000);
            string Z = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertZ = Convert.ToInt64(Z);                                                                  //снятие данных масштаба 1
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_8")).Click();                                      //"уменьшить масштаб"
            Thread.Sleep(2000);
            string Z1 = (string)js.ExecuteScript("return window.portal.stdmap.map.getZoom().toString()");
            long convertZ1 = Convert.ToInt64(Z1);                                                                //снятие данных масштаба 2
            if (convertZ <= convertZ1)                                                                           //проверка
                Assert.Fail("после клика ");
            double zoom2 = Convert.ToDouble(convertZ1);
            if (zoom2 + 1 != convertZ)
                Assert.Fail("уровень Zoom'а не уменьшился на еденицу");                                                         //сообщение о неудачнй проверке


        }
        [TestCleanup]
        public void Clean()
        {
            Thread.Sleep(2000);
            driver.Quit();
        }
    }
}
