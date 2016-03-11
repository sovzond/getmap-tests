using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Газовая инфраструктура',
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class GasStructLayer
    {
        private IWebDriver driver;
        private const string gpzPoint = "ГПЗ (точка)";
        private const string gazoprovod = "Газопровод";
        private const string gpzPoligon = "ГПЗ (полигон)";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxs;

        private GasStructLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private GasStructLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private GasStructLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "ГПЗ (точка)")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gpzPoint,listCheckBoxs[i - 1]);
                    dicSB.Add(gpzPoint,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Газопровод")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gazoprovod,listCheckBoxs[i - 1]);
                    dicSB.Add(gazoprovod,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "ГПЗ (полигон)")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gpzPoligon,listCheckBoxs[i - 1]);
                    dicSB.Add(gpzPoligon,listCheckBoxs[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static GasStructLayer get(IWebDriver driver)
        {
            return new GasStructLayer(driver);
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'ГПЗ (точка)'.
        /// </summary>
        /// <returns></returns>
        public GasStructLayer GPZPointClick()
        {
            dicCB[gpzPoint].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ГПЗ (точка)'.
        /// </summary>
        /// <returns></returns>
        public GasStructLayer GPZPointSBClick()
        {
            dicSB[gpzPoint].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Газопровод'.
        /// </summary>
        /// <returns></returns>
        public GasStructLayer GazoprovodClick()
        {
            dicCB[gazoprovod].Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Газопровод'.
        /// </summary>
        /// <returns></returns>
        public GasStructLayer GazoprovodSBClick()
        {
            dicSB[gazoprovod].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'ГПЗ (полигон)'.
        /// </summary>
        /// <returns></returns>
        public GasStructLayer GPZPoligonClick()
        {
            dicCB[gpzPoligon].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ГПЗ (полигон)'.
        /// </summary>
        /// <returns></returns>
        public GasStructLayer GPZPoligonSBClick()
        {
            dicSB[gpzPoligon].Click();
            return this;
        }
    }
}
