using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Тематические карты', 
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class TematicMapLayer
    {
        private IWebDriver driver;
        private const string prs06 = "Площадки разведочной скважины 2006 г.";
        private const string prs08 = "Площадки разведочной скважины 2008 г.";
        private const string nr06 = "Нефтяные разливы 2006 г.";
        private const string nr08 = "Нефтяные разливы 2008 г.";
        private const string gk06 = "Гидронамывные карьеры 2006 г.";
        private const string gk08 = "Гидронамывные карьеры 2008 г.";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;
        private IList<IWebElement> listCheckBoxs;

        private TematicMapLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private TematicMapLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private TematicMapLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "Площадки разведочной скважины 2006 г.")
                {
                    Thread.Sleep(200);
                    dicCB.Add(prs06, listCheckBoxs[i - 1]);
                    dicSB.Add(prs06, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Площадки разведочной скважины 2008 г.")
                {
                    Thread.Sleep(200);
                    dicCB.Add(prs08, listCheckBoxs[i - 1]);
                    dicSB.Add(prs08, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Нефтяные разливы 2006 г.")
                {
                    Thread.Sleep(200);
                    dicCB.Add(nr06, listCheckBoxs[i - 1]);
                    dicSB.Add(nr06, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Нефтяные разливы 2008 г.")
                {
                    Thread.Sleep(200);
                    dicCB.Add(nr08,listCheckBoxs[i - 1]);
                    dicSB.Add(nr08, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Гидронамывные карьеры 2006 г.")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gk06,listCheckBoxs[i - 1]);
                    dicSB.Add(gk06, listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Гидронамывные карьеры 2008 г.")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gk08, listCheckBoxs[i - 1]);
                    dicSB.Add(gk08, listCheckBoxs[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static TematicMapLayer get(IWebDriver driver)
        {
            return new TematicMapLayer(driver);
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Площадки разведочной скважины 2006 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Prs06Click()
        {
            dicCB[prs06].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Площадки разведочной скважины 2006 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Prs06SBClick()
        {
            dicSB[prs06].Click();
            return this;
        }
        /// <summary>
        /// Выполняет клик по чекбоксу 'Площадки разведочной скважины 2008 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Prs08Click()
        {
           dicCB[prs08].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Площадки разведочной скважины 2008 г.'
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Prs08SBClick()
        {
            dicSB[prs08].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Нефтяные разливы 2006 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Nr06Click()
        {
            dicCB[nr06].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Нефтяные разливы 2006 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Nr06SBClick()
        {
            dicSB[nr06].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Нефтяные разливы 2008 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Nr08Click()
        {
            dicCB[nr08].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Нефтяные разливы 2008 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Nr08SBClick()
        {
            dicSB[nr08].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Гидронамывные карьеры 2006 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Gk06Click()
        {
            dicCB[gk06].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Гидронамывные карьеры 2006 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Gk06SBClick()
        {
            dicSB[gk06].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Гидро намывные карьеры 2008 г.'.
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Gk08Click()
        {
            dicCB[gk08].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Гидронамывные карьеры 2008 г.'
        /// </summary>
        /// <returns></returns>
        public TematicMapLayer Gk08SBClick()
        {
            dicSB[gk08].Click();
            return this;
        }
    }
}
