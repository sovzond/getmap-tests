using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Зоя. Тест',
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class ZoyaTestLayer
    {
        private IWebDriver driver;
        private const string mo_re = "GetMap_MO_RE";
        private const string l8_mo = "GetMap_L8_MO";
        private const string locationCheckBoxes = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxes;

        private ZoyaTestLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private ZoyaTestLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxes = driver.FindElements(By.CssSelector(locationCheckBoxes));
            return this;
        }

        private ZoyaTestLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxes.Count; i++)
            {
                if (listCheckBoxes[i].Text == "GetMap_MO_RE")
                {
                    Thread.Sleep(200);
                    dicCB.Add(mo_re, listCheckBoxes[i - 1]);
                    dicSB.Add(mo_re, listCheckBoxes[i + 1]);
                }
                if (listCheckBoxes[i].Text == "GetMap_L8_MO")
                {
                    Thread.Sleep(200);
                    dicCB.Add(l8_mo, listCheckBoxes[i - 1]);
                    dicSB.Add(l8_mo, listCheckBoxes[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static ZoyaTestLayer get(IWebDriver driver)
        {
            return new ZoyaTestLayer(driver);
        }

        /// <summary>
        /// Выполянет клик по чекбоксу 'GetMap_MO_RE'.
        /// </summary>
        /// <returns></returns>
        public ZoyaTestLayer MO_REClick()
        {
            dicCB[mo_re].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'GetMap_MO_RE'.
        /// </summary>
        /// <returns></returns>
        public ZoyaTestLayer MO_RESBClick()
        {
            dicSB[mo_re].Click();
            return this;
        }

        /// <summary>
        /// Выполянет клик по чекбоксу 'GetMap_L8_MO'.
        /// </summary>
        /// <returns></returns>
        public ZoyaTestLayer L8_MOClick()
        {
            dicCB[l8_mo].Click();
            return this;
        }

        /// <summary>
        ///Выполняет клик по кнопке 'Настройка слоя' слоя 'GetMap_L8_MO'.
        /// </summary>
        /// <returns></returns>
        public ZoyaTestLayer L8_MOSBClick()
        {
            dicSB[l8_mo].Click();
            return this;
        }

    }
}
