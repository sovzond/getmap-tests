using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    public class InputCoordWnd
    {
        private IWebDriver driver;

        private InputCoordWnd(IWebDriver driver)
        {
            this.driver = driver;
        }
        static public InputCoordWnd get(IWebDriver driver)
        {
            driver.FindElement(By.ClassName("gotoCoordsButton")).Click();
            return new InputCoordWnd(driver);
        }
        public InputCoordWnd setLon(int degrees, int minutes, int seconds)
        {
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys(degrees.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys(minutes.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys(seconds.ToString());
            return this;
        }
        public InputCoordWnd setLat(int degrees, int minutes, int seconds)
        {
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys(degrees.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys(minutes.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys(seconds.ToString());
            return this;
        }
        public void click()
        {
            driver.FindElement(By.CssSelector("#gotoCoords .button")).Click();
        }     
    }
}
