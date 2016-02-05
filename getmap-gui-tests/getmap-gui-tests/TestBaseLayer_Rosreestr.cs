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
        private IWebDriver driver;       
        [TestMethod]
        public void TestRosreestr()
        {
            driver = Settings.Instance.createDriver();
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();
            Thread.Sleep(5000);
            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));
            var builder = new Actions(driver);
            builder.Click(element).Perform();
            IList<IWebElement> el = driver.FindElements(By.ClassName("svzLayerManagerItem"));
            for (int n = 0; n < el.Count; n++)
            {
                if (el[0].Text != "Google") Assert.Fail("не найден Google");
                if (el[4].Text != "Росреестр") Assert.Fail("не найден Росреестр");
                if (el[5].Text != "OpenStreetMap") Assert.Fail("не найден OpenStreetMap");
            }
                IWebElement element1 = driver.FindElement(By.Id("dijit_form_RadioButton_3"));
                builder.Click(element1).Perform();
               
               Thread.Sleep(5000);  
               string h= (string)js.ExecuteScript("return window.portal.stdmap.map.baseLayer.url.toString()");
               if( h.StartsWith("http://maps.rosreestr.ru/")!= true) Assert.Fail("не отображен базовый слой Росреестр");
        }
    }
}
