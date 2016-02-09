using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет заполнение ячеек С.Ш. и В.Д. для перехода по координатам на сайте.
    /// </summary>
    public class InputCoordWnd
    {
        private IWebDriver driver;

        private InputCoordWnd(IWebDriver driver)
        {
            this.driver = driver;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Переход по координатам', а так же 
        /// передает параметр закрытому конструктору типа InputCoordWnd
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        static public InputCoordWnd get(IWebDriver driver)
        {
            driver.FindElement(By.ClassName("gotoCoordsButton")).Click();
            return new InputCoordWnd(driver);
        }
        /// <summary>
        /// Заполняет ячейки С.Ш. для перехода по координатам.
        /// </summary>
        /// <param name="degrees">Введите градусы</param>
        /// <param name="minutes">Введите минуты</param>
        /// <param name="seconds">Введите секунды</param>
        /// <returns></returns>
        public InputCoordWnd setLon(int degrees, int minutes, int seconds)
        {
            driver.FindElement(By.Id("dijit_form_NumberTextBox_0")).SendKeys(degrees.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_1")).SendKeys(minutes.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_2")).SendKeys(seconds.ToString());
            return this;
        }
        /// <summary>
        /// Заполняет ячейки В.Д для перехода по координатам. 
        /// </summary>
        /// <param name="degrees">Введите градусы</param>
        /// <param name="minutes">Введите минуты</param>
        /// <param name="seconds">Введите секунды</param>
        /// <returns></returns>
        public InputCoordWnd setLat(int degrees, int minutes, int seconds)
        {
            driver.FindElement(By.Id("dijit_form_NumberTextBox_3")).SendKeys(degrees.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_4")).SendKeys(minutes.ToString());
            driver.FindElement(By.Id("dijit_form_NumberTextBox_5")).SendKeys(seconds.ToString());
            return this;
        }
        /// <summary>
        /// Осуществляет клик по кнопке 'Найти'.
        /// </summary>
        public void click()
        {
            driver.FindElement(By.CssSelector("#gotoCoords .button")).Click();
        }     
    }
}
