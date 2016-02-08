using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
namespace GetMapTest
{
    [TestClass]
    public class TestBaseLayer_Rosreestr
    {
        private bool getElementByText(IList<IWebElement> els, string text)
        {
            foreach (IWebElement el in els)
            {
                if (el.Text == text)
                {
                    return true;
                }              
            }
            return false;
        }
        private void AssertGetElementByText()
        {
            IList<IWebElement> el = driver.FindElements(By.ClassName("svzLayerManagerItem"));
            if (getElementByText(el, "Google") == false) Assert.Fail("не найден Google");
            if (getElementByText(el, "Росреестр") == false) Assert.Fail("не найден Росреестр");
            if (getElementByText(el, "OpenStreetMap") == false) Assert.Fail("не найден OpenStreetMap");
        }
        private Boolean assertURL()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
          string  URL=(string)js.ExecuteScript("return window.portal.stdmap.map.baseLayer.url.toString()");
            if (URL.StartsWith("http://maps.rosreestr.ru/") != true)
            {
                return true;
            }
            {
                return false;
            }
        }
        private string getId()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            return (string)js.ExecuteScript("return window.portal.stdmap.map.baseLayer.div.id.toString()");
        }
        private IWebDriver driver;       
        [TestMethod]
        public void TestRosreestr()
        {
            driver = Settings.Instance.createDriver();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);          
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();
            Thread.Sleep(5000);
            var builder = new Actions(driver);
            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));            
            builder.Click(element).Perform();
            AssertGetElementByText();
            IWebElement element1 = driver.FindElement(By.Id("dijit_form_RadioButton_3"));
                builder.Click(element1).Perform();               
               Thread.Sleep(5000);
            if (assertURL() == true)
            {
                Assert.Fail("не показан файл из http://maps.rosreestr.ru/ ");
            }              
               string id = getId();
           /* IWebElement element2 = driver.FindElement(By.Id(id));*/
            IList < IWebElement> element2 =driver.FindElements(By.CssSelector("div.olMap img[src*='http://maps.rosreestr.ru/']"));
            List<string> listAttributeSrc = new List<string>();
            foreach(var el in element2)
            {
                listAttributeSrc.Add(el.GetAttribute("src"));
            }

          
        }
    }
}

