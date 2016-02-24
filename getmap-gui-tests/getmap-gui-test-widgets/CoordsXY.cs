using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем типам координат в нижнем левом углу.
    /// </summary>
   public class CoordsXY
    {
        private IWebDriver driver;
        private const string locationCoordsValue = "#dCoord";
        private const string locationElementsP = "#dCoord p";
        private IWebElement elementDMS;
        private IWebElement elementDecimal;
        private IWebElement elementMeteres;
        private IList<IWebElement> listElementsP;

        private CoordsXY(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();      
        }

        private CoordsXY SetValueList()
        {
            listElementsP = driver.FindElements(By.CssSelector(locationElementsP));
            return this;
        }

        private CoordsXY SetValueElements()
        { 
           for(int i=0;i<listElementsP.Count;i++)
            {
                    elementDMS = listElementsP[0];
                    elementDecimal = listElementsP[1];
                    elementMeteres = listElementsP[2];
            }
            return this;
        }
        
        private void Sleep()
        {
            System.Threading.Thread.Sleep(1000);
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static CoordsXY get(IWebDriver driver)
        {
            return new CoordsXY(driver);
        }

        /// <summary>
        /// Выполняет клик по элементу 'Градусы, минуты, секунды'.
        /// </summary>
        /// <returns></returns>
        public CoordsXY DegreeMinutesSecondsClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationCoordsValue)).Click();
            Sleep();
            elementDMS.Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по элементу 'Десятичные градусы'.
        /// </summary>
        /// <returns></returns>
        public CoordsXY DecimalClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationCoordsValue)).Click();
            Sleep();
            elementDecimal.Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по элементу 'Метры'.
        /// </summary>
        /// <returns></returns>
        public CoordsXY MetresClick()
        {
            Sleep();
            driver.FindElement(By.CssSelector(locationCoordsValue)).Click();
            Sleep();
            elementMeteres.Click();
            return this;
        }
    }
}
