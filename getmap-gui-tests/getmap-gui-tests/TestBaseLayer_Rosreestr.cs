using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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

        private void g()
        {
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();
            Thread.Sleep(5000);
            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));
            var builder = new Actions(driver);
            builder.Click(element).Perform();
        }



        private IWebDriver driver;       
        [TestMethod]
        public void TestRosreestr()
        {
            driver = Settings.Instance.createDriver();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            g();
           driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();
            Thread.Sleep(5000);
            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));
            var builder = new Actions(driver);
            builder.Click(element).Perform();
            AssertGetElementByText();
            IWebElement element1 = driver.FindElement(By.Id("dijit_form_RadioButton_3"));
                builder.Click(element1).Perform();               
               Thread.Sleep(5000);  
               string URL= (string)js.ExecuteScript("return window.portal.stdmap.map.baseLayer.url.toString()");
               if(URL.StartsWith("http://maps.rosreestr.ru/")!= true) Assert.Fail("не отображен базовый слой Росреестр");
               string u = (string)js.ExecuteScript("return window.portal.stdmap.map.baseLayer.div.id.toString()");
        }
    }
}
