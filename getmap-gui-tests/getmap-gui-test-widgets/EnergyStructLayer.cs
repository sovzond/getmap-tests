using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Дает доступ ко всем чекбоксам выпадающего меню 'Энергетичская инфраструктура',
    /// а так же кнопке 'Настройка слоя'.
    /// </summary>
    public class EnergyStructLayer
    {
        private IWebDriver driver;
        private IList<IWebElement> listCheckBoxs;
        private const string electroSationPoint = "Электростанции (точка)";
        private const string podstationPoint = "Подстанции (точка)";
        private const string lep = "ЛЭП";
        private const string podstationPoligon = "Электростанции (полигон)";
        private const string electroStationPoligon = "Подстанции (полигон)";
        private const string locationCheckBoxs = "div.svzLayerManagerItem.svzLayerManagerItem1 div";
        private Dictionary<string, IWebElement> dicCB;
        private Dictionary<string, IWebElement> dicSB;

        private EnergyStructLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private EnergyStructLayer SetValueList()
        {
            dicCB = new Dictionary<string, IWebElement>();
            dicSB = new Dictionary<string, IWebElement>();
            listCheckBoxs = driver.FindElements(By.CssSelector(locationCheckBoxs));
            return this;
        }

        private EnergyStructLayer SetValueElements()
        {
            for (int i = 0; i < listCheckBoxs.Count; i++)
            {
                if (listCheckBoxs[i].Text == "Электростанции (точка)")
                {
                    Thread.Sleep(200);
                    dicCB.Add(electroSationPoint,listCheckBoxs[i - 1]);
                    dicSB.Add(electroSationPoint,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Подстанции (точка)")
                {
                    Thread.Sleep(200);
                    dicCB.Add(podstationPoint,listCheckBoxs[i - 1]);
                    dicSB.Add(podstationPoint,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "ЛЭП")
                {
                    Thread.Sleep(200);
                    dicCB.Add(lep,listCheckBoxs[i - 1]);
                    dicSB.Add(lep,listCheckBoxs[i + 1]);
                }
                if (listCheckBoxs[i].Text == "Электростанции (полигон)")
                {
                    Thread.Sleep(200);
                    dicCB.Add(electroStationPoligon,listCheckBoxs[i - 1]);
                    dicSB.Add(electroStationPoligon,listCheckBoxs[i + 1]);

                }
                if (listCheckBoxs[i].Text == "Подстанции (полигон)")
                {
                    Thread.Sleep(200);
                    dicCB.Add(podstationPoligon,listCheckBoxs[i - 1]);
                    dicSB.Add(podstationPoligon,listCheckBoxs[i + 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static EnergyStructLayer get(IWebDriver driver)
        {
            return new EnergyStructLayer(driver);
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Электростанция (точка)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer ElectroStationPointClick()
        {
            dicCB[electroSationPoint].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Электростанция (точка)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer ElectroStationPointSBClick()
        {
            dicSB[electroSationPoint].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Подстанция (точка)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer PodstationPointClick()
        {
            dicCB[podstationPoint].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Подстанция (точка)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer PodstationPointSBClick()
        {
            dicSB[podstationPoint].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'ЛЭП'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer LEPClick()
        {
            dicCB[lep].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'ЛЭЫ'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer LEPSBClick()
        {
            dicSB[lep].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Электростанция (полигон)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer ElectroStationPoligonClick()
        {
            dicCB[electroStationPoligon].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Электростанция (полигон)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer ElectroStationPoligonSBClick()
        {
            dicSB[electroStationPoligon].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу 'Подстанции (полигон)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer PodstationPoligon()
        {
            dicCB[podstationPoligon].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по кнопке 'Настройка слоя' слоя 'Полстанции (полигон)'.
        /// </summary>
        /// <returns></returns>
        public EnergyStructLayer PodstationPoligonSBClick()
        {
            dicSB[podstationPoligon].Click();
            return this;
        }

    }
}
