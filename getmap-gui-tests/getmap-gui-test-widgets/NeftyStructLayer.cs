using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Нефтяная инфраструктура',
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class NeftyStructLayer
    {
        private IWebDriver driver;
        private const string fakel = "Факелы";
        private const string ambar = "Амбары";
        private const string places = "Кустовые площадки";
        private const string dns = "ДНС";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxs;

        private NeftyStructLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private NeftyStructLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private NeftyStructLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "Факелы")
                {
                    Thread.Sleep(200);
                    dicCB.Add(fakel,listCheckBoxs[i - 1]);
                    dicSB.Add(fakel,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Амбары")
                {
                    Thread.Sleep(200);
                    dicCB.Add(ambar,listCheckBoxs[i - 1]);
                    dicSB.Add(ambar,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Кустовые площадки")
                {
                    Thread.Sleep(200);
                    dicCB.Add(places,listCheckBoxs[i - 1]);
                    dicSB.Add(places,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "ДНС")
                {
                    Thread.Sleep(200);
                    dicCB.Add(dns,listCheckBoxs[i - 1]);
                    dicSB.Add(dns,listCheckBoxs[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static NeftyStructLayer get(IWebDriver driver)
        {
            return new NeftyStructLayer(driver);
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Факелы'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer FakelClick()
        {
            dicCB[fakel].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Факелы'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer FakelSBClick()
        {
            dicSB[fakel].Click();
            return this;
        }


        /// <summary>
        /// Выполняет клик по чекбоксу 'Амбары'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer AmbarClick()
        {
            dicCB[ambar].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Амбары'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer AmbarSBClick()
        {
            dicSB[ambar].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Кустовые площадки'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer PlacesClick()
        {
           dicCB[places].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Кустовые площадки'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer PlacesSBClick()
        {
            dicSB[places].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'ДНС'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer DNSClick()
        {
            dicCB[dns].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ДНС'.
        /// </summary>
        /// <returns></returns>
        public NeftyStructLayer DNSSBClick()
        {
            dicSB[dns].Click();
            return this;
        }

    }
}
