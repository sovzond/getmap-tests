using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;

namespace GetMapTest.GUI
{
    /// <summary>
    /// На плажке 'Данные и карты' дает доступ к чекбоксам выпадающего меню раздела 'Тематические слои',
    /// соответственно открывает выпадающее меню или же выполняет клик по чекбоксу.
    /// </summary>
    public class Layers
    {
        private IWebDriver driver;
        private const string newGroup = "Новая группа";
        private const string test = "Тест";
        private const string zoyaTest = "Зоя. Тест";
        private const string moscowArea = "Московская область";
        private const string gasStruct = "Газовая инфраструктура";
        private const string energyStruct = "Энергетическая инфраструктура";
        private const string neftyStrcut = "Нефтяная инфраструктура";
        private const string tematicMap = "Тематические карты";
        private const string cosmoPhoto = "Космические снимки";
        private const string locationDropDownMenu = "div.svzLayerManagerText";
        private const string locationCheckBoxes = "div.svzLayerManagerItem.svzLayerManagerItemSection div";
        private Dictionary<string, IWebElement> dicDDM;
        private Dictionary<string, IWebElement> dicCB;
        private IList<IWebElement> listDropDowmMenu;
        private IList<IWebElement> listCheckBoxes;

        private Layers(IWebDriver driver)
        {
            this.driver = driver;
            SetValueList();
            SetValueElements();
        }

        private Layers SetValueList()
        {
            dicDDM = new Dictionary<string, IWebElement>();
            dicCB = new Dictionary<string, IWebElement>();
            listDropDowmMenu = driver.FindElements(By.CssSelector(locationDropDownMenu));
            listCheckBoxes = driver.FindElements(By.CssSelector(locationCheckBoxes));
            return this;
        }

        private Layers SetValueElements()
        {
            foreach (var el in listDropDowmMenu)
            {
                if (el.Text == "Тест")
                    dicDDM.Add(test, el);
                if (el.Text == "Новая группа")
                    dicDDM.Add(newGroup, el);
                if (el.Text == "Зоя. Тест")
                    dicDDM.Add(zoyaTest, el);
                if (el.Text == "Московская область")
                    dicDDM.Add(moscowArea, el);
                if (el.Text == "Газовая инфраструктура")
                    dicDDM.Add(gasStruct, el);
                if (el.Text == "Энергетическая инфраструктура")
                    dicDDM.Add(energyStruct, el);
                if (el.Text == "Нефтяная инфраструктура")
                    dicDDM.Add(neftyStrcut, el);
                if (el.Text == "Тематические карты")
                    dicDDM.Add(tematicMap, el);
                if (el.Text == "Космические снимки")
                    dicDDM.Add(cosmoPhoto, el);
            }
            for (int i = 0; i < listCheckBoxes.Count; i++)
            {
                if (listCheckBoxes[i].Text == "Тест")
                {
                    Thread.Sleep(200);
                    dicCB.Add(test, listCheckBoxes[i - 1]);                  
                }
                if (listCheckBoxes[i].Text == "Новая группа")
                {
                    Thread.Sleep(200);
                    dicCB.Add(newGroup, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Зоя. Тест")
                {
                    Thread.Sleep(200);
                    dicCB.Add(zoyaTest, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Московская область")
                {
                    Thread.Sleep(200);
                    dicCB.Add(moscowArea, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Газовая инфраструктура")
                {
                    Thread.Sleep(200);
                    dicCB.Add(gasStruct, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Энергетическая инфраструктура")
                {
                    Thread.Sleep(200);
                    dicCB.Add(energyStruct, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Нефтяная инфраструктура")
                {
                    Thread.Sleep(200);
                    dicCB.Add(neftyStrcut, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Тематические карты")
                {
                    Thread.Sleep(200);
                    dicCB.Add(tematicMap, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Космические снимки")
                {
                    Thread.Sleep(200);
                    dicCB.Add(cosmoPhoto, listCheckBoxes[i - 1]);
                }
            }
            return this;
        }

        /// <summary>
        /// Принимает параметр типа IWebDriver для дальнейшей навигации по сайту.
        /// </summary>
        /// <param name="driver">Передает аргумент для закрытого конструктора</param>
        /// <returns></returns>
        public static Layers get(IWebDriver driver)
        {
            return new Layers(driver);

        }

        private bool getSelectedTest()
        {
            if (dicDDM[test].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Тест' активен.
        /// </summary>
        public bool GetSelectedTest
        {
            get
            {
                return getSelectedTest();
            }
        }

        private bool getSelectedNewGroup()
        {
            if (dicDDM[newGroup].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Новая группа' активен.
        /// </summary>
        public bool GetSelectedNewGroup
        {
            get
            {
                return getSelectedNewGroup();
            }
        }

        private bool getSelectedZoyaTest()
        {
            if (dicDDM[zoyaTest].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Зоя. Тест' активен.
        /// </summary>
        public bool GetSelectedZoyaTest
        {
            get
            {
                return getSelectedZoyaTest();
            }
        }

        private bool getSelectedMoscowArea()
        {
            if (dicDDM[moscowArea].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Московская область' активен.
        /// </summary>
        public bool GetSelectedMoscowArea
        {
            get
            {
                return getSelectedMoscowArea();
            }
        }

        private bool getSelectedGasStruct()
        {
            if (dicDDM[gasStruct].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Газовая инфраструктура' активен.
        /// </summary>
        public bool GetSelectedGasStruct
        {
            get
            {
                return getSelectedGasStruct();
            }
        }

        private bool getSelectedEnergyStruct()
        {
            if (dicDDM[energyStruct].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Энергетическая инфраструктура' активен.
        /// </summary>
        public bool GetSelectedEnergyStruct
        {
            get
            {
                return getSelectedEnergyStruct();
            }
        }

        private bool getSelectedNeftyStruct()
        {
            if (dicDDM[neftyStrcut].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Нефтяная инфраструктура' активен.
        /// </summary>
        public bool GetSelectedNeftyStruct
        {
            get
            {
                return getSelectedNeftyStruct();
            }
        }
        private bool getSelectedTimaticMap()
        {
            if (dicDDM[tematicMap].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Тематические карты' активен.
        /// </summary>
        public bool GetSelectedTematicMap
        {
            get
            {
                return getSelectedTimaticMap();
            }
        }

        private bool getSelectedCosmoPhoto()
        {
            if (dicDDM[cosmoPhoto].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Космические снимки' активен.
        /// </summary>
        public bool GetSelectedCosmoPhoto
        {
            get
            {
                return getSelectedCosmoPhoto();
            }
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers TestCheckBoxClick()
        {
            dicCB[test].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers TestOpenCloseList()
        {
            dicDDM[test].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupCheckBoxClick()
        {
            dicCB[newGroup].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupOpenCloseList()
        {
            dicDDM[newGroup].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestCheckBoxClick()
        {
            dicCB[zoyaTest].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestOpenCloseList()
        {
            dicDDM[zoyaTest].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaCheckBoxClick()
        {
            dicCB[moscowArea].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaOpenCloseList()
        {
            dicDDM[moscowArea].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructCheckBoxClick()
        {
            dicCB[gasStruct].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructOpenCloseList()
        {
            dicDDM[gasStruct].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStructCheckBoxClick()
        {
            dicCB[energyStruct].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStructOpenCloseList()
        {
            dicDDM[energyStruct].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructCheckBoxClick()
        {
            dicCB[neftyStrcut].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructOpenCloseList()
        {
            dicDDM[neftyStrcut].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapCheckBoxClick()
        {
            dicCB[tematicMap].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapOpenCloseList()
        {

            dicDDM[tematicMap].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Космические снимки'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoCheckBoxClick()
        {
            dicCB[cosmoPhoto].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Космические снимки.'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoOpenCloseList()
        {
            dicDDM[cosmoPhoto].Click();
            return this;
        }
    }
}
