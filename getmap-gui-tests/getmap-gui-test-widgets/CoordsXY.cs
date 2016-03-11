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
        private const string dms = "Градусы, минуты, секунды";
        private const string decimaL = "Десятичные градусы";
        private const string metres = "Метры";
        private Dictionary<string, IWebElement> dicElementsP;
        private IList<IWebElement> listElementsP;

        private CoordsXY(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();      
        }

        private CoordsXY SetValueList()
        {
            dicElementsP = new Dictionary<string, IWebElement>();
            driver.FindElement(By.CssSelector(locationCoordsValue)).Click();
            System.Threading.Thread.Sleep(200);
            listElementsP = driver.FindElements(By.CssSelector(locationElementsP));
            return this;
        }

        private CoordsXY SetValueElements()
        { 
           for(int i=0;i<listElementsP.Count;i++)
            {
                if (listElementsP[i].Text == "Градусы, минуты, секунды")
                    dicElementsP.Add(dms, listElementsP[i]);
                if (listElementsP[i].Text == "Десятичные градусы")
                    dicElementsP.Add(decimaL, listElementsP[i]);
                if (listElementsP[i].Text == "Метры")
                    dicElementsP.Add(metres, listElementsP[i]);
            }
            return this;
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
            dicElementsP[dms].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по элементу 'Десятичные градусы'.
        /// </summary>
        /// <returns></returns>
        public CoordsXY DecimalClick()
        {
            dicElementsP[decimaL].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по элементу 'Метры'.
        /// </summary>
        /// <returns></returns>
        public CoordsXY MetresClick()
        {
            dicElementsP[metres].Click();
            return this;
        }
    }
}
