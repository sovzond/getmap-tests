using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// Выполняет клик по векторным кнопкам любого слоя.
    /// </summary>
    public class VectorButtonsLayer
    {
        private IWebDriver driver;
        private IWebElement elementStatisticsLayer;
        private IWebElement elementZoomToLayerExtext;
        private IWebElement elementEditLayer;
        private const string locationButtonsVectorLayer = "div.userLayerMenuContainer.userLayerMenuContainerActive > div.svzSimpleButton";
        private IList<IWebElement> listButtonsVectorLayer;
        private enum NumberButtons
        {
            Statistic = 1,
            Zoom = 0,
            Edit = 2
        }

        private VectorButtonsLayer(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private VectorButtonsLayer SetValueList()
        {
            listButtonsVectorLayer = driver.FindElements(By.CssSelector(locationButtonsVectorLayer));
            return this;
        }

        private VectorButtonsLayer SetValueElements()
        {
            elementStatisticsLayer = listButtonsVectorLayer[(int)NumberButtons.Statistic];
            elementZoomToLayerExtext = listButtonsVectorLayer[(int)NumberButtons.Zoom];
            if (listButtonsVectorLayer.Count > 2)
                elementEditLayer = listButtonsVectorLayer[(int)NumberButtons.Edit];
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static VectorButtonsLayer get(IWebDriver driver)
        {
            return new VectorButtonsLayer(driver);
        }

        /// <summary>
        /// Выполянет клик по векторной кнопке 'Статистика слоя'.
        /// </summary>
        /// <returns></returns>
        public VectorButtonsLayer StatisticsLayerClick()
        {
            elementStatisticsLayer.Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по векторной  кнопке 'Приближение к экстенту слоя'.
        /// </summary>
        /// <returns></returns>
        public VectorButtonsLayer ZoomToLayerExtent()
        {
            elementZoomToLayerExtext.Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по вектороной кнопке 'Редактировать слой'.
        /// </summary>
        /// <returns></returns>
        public VectorButtonsLayer EditLayer()
        {
            elementEditLayer.Click();
            return this;
        }
    }
}
