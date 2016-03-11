using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Новая группа', 
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class NewGroupLayer
    {
        private IWebDriver driver;
        private const string tsp_25 = "tsp_25";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxs;

        private NewGroupLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static NewGroupLayer get(IWebDriver driver)
        {
            return new NewGroupLayer(driver);
        }

        private NewGroupLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private NewGroupLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "tsp_25")
                {
                    Thread.Sleep(200);
                    dicCB.Add(tsp_25,listCheckBoxs[i - 1]);
                    dicSB.Add(tsp_25,listCheckBoxs[i + 1]);
                    break;
                }
            }

            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'tsp_25'.
        /// </summary>
        /// <returns></returns>
        public NewGroupLayer Tsp_25Click()
        { 
            dicCB[tsp_25].Click();
            return this;
        }
        /*
                    /// <summary>
                    /// Выполняет клик по кнопке 'Настройка слоя' слоя 'tsp_25'.
                    /// </summary>
                    /// <returns></returns>
                    public NewGroupClass Tsp_25SBClick()
                    {
                        dicSB[tsp_25].Click();
                        return this;
                    }
                    */
    }
}
