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
        private List<string> listAttributeSrcOpen()
        {
            IList<IWebElement> element = driver.FindElements(By.CssSelector("div.olMap img[src*='http://c.tile.openstreetmap.org']"));
            IList<IWebElement> element1 = driver.FindElements(By.CssSelector("div.olMap img[src*='http://a.tile.openstreetmap.org']"));
            IList<IWebElement> element2 = driver.FindElements(By.CssSelector("div.olMap img[src*='http://b.tile.openstreetmap.org']"));
            List<string> listAttributeSrc = new List<string>();
            foreach (var el in element)
            {
                listAttributeSrc.Add(el.GetAttribute("src"));
            }
            foreach (var el in element1)
            {
                listAttributeSrc.Add(el.GetAttribute("src"));
            }
            foreach (var el in element2)
            {
                listAttributeSrc.Add(el.GetAttribute("src"));
            }
            return listAttributeSrc;
        }
        private Boolean AssertAttributeSrcOpen(string ListAttributeSrc)
        {
            if (ListAttributeSrc.StartsWith("http://c.tile.openstreetmap.org") != true && ListAttributeSrc.StartsWith("http://a.tile.openstreetmap.org") != true && ListAttributeSrc.StartsWith("http://b.tile.openstreetmap.org") != true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
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
        private Boolean AssertAttributeSrcRos(string ListAttributeSrc)
        {
            if (ListAttributeSrc.StartsWith("http://maps.rosreestr.ru/") != true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    private List<string> listAttributeSrcRos()
        {  
            IList<IWebElement> element2 = driver.FindElements(By.CssSelector("div.olMap img[src*='http://maps.rosreestr.ru']"));
            List<string> listAttributeSrc = new List<string>();
            foreach (var el in element2)
            {
                listAttributeSrc.Add(el.GetAttribute("src"));
            }
            return listAttributeSrc;
        }    
        private void AssertGetElementByText()
        {
            IList<IWebElement> el = driver.FindElements(By.ClassName("svzLayerManagerItem"));
            if (getElementByText(el, "Google") == false) Assert.Fail("не найден Google");
            if (getElementByText(el, "Росреестр") == false) Assert.Fail("не найден Росреестр");
            if (getElementByText(el, "OpenStreetMap") == false) Assert.Fail("не найден OpenStreetMap");
        }              
        [TestMethod]
        public void TestRosreestr()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);           
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();//открывает слои
            Thread.Sleep(4000);
            var builder = new Actions(driver);
            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));         
            builder.Click(element).Perform(); //открывает базовые слои  
            AssertGetElementByText();
            IWebElement element1 = driver.FindElement(By.Id("dijit_form_RadioButton_3"));
            builder.Click(element1).Perform(); //открывает Росреестр               
            Thread.Sleep(4000);
            List<string> ListAttributeSrc = listAttributeSrcRos();
            for (int n = 0; n < ListAttributeSrc.Count; n++)
            {
                if (AssertAttributeSrcRos(ListAttributeSrc[n]) != true)
                {
                    Assert.Fail("не показан файл из росреестра ");
                }
            }
        }
        [TestMethod]
        public void TestOpenStreetMap()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.loginAsGuest(driver, Settings.Instance.BaseUrl);
            driver.FindElement(By.Id("sovzond_widget_SimpleButton_74")).Click();//открывает слои
            Thread.Sleep(4000);
            var builder = new Actions(driver);
            IWebElement element = driver.FindElement(By.Id("sovzond_widget_SimpleButton_0"));
            builder.Click(element).Perform();//открывает базовые слои  
            IWebElement element1 = driver.FindElement(By.Id("dijit_form_RadioButton_4"));
            builder.Click(element1).Perform(); //открывает OpenStreetMap   
            Thread.Sleep(4000);
            List<string> ListAttributeSrc = listAttributeSrcOpen();
            for (int n = 0; n < ListAttributeSrc.Count; n++)
            {
                if (AssertAttributeSrcOpen(ListAttributeSrc[n]) != true)
                {
                    Assert.Fail("не показан файл из openSteetMap");
                }
            }
        }
    }
}

