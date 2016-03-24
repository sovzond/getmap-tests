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
        private const string _newGroup = "Новая группа";
        private const string _newGroup2 = "Новая группа 2";
        private const string _test = "Тест";
        private const string _zoyaTest = "Зоя. Тест";
        private const string _moscowArea = "Московская область";
        private const string _gasStruct = "Газовая инфраструктура";
        private const string _energyStruct = "Энергетическая инфраструктура";
        private const string _neftyStrcut = "Нефтяная инфраструктура";
        private const string _tematicMap = "Тематические карты";
        private const string _cosmoPhoto = "Космические снимки";
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
                    dicDDM.Add(_test, el);
                if (el.Text == "Новая группа 2")
                    dicDDM.Add(_newGroup2, el);
                if (el.Text == "Новая группа")
                    dicDDM.Add(_newGroup, el);
                if (el.Text == "Зоя. Тест")
                    dicDDM.Add(_zoyaTest, el);
                if (el.Text == "Московская область")
                    dicDDM.Add(_moscowArea, el);
                if (el.Text == "Газовая инфраструктура")
                    dicDDM.Add(_gasStruct, el);
                if (el.Text == "Энергетическая инфраструктура")
                    dicDDM.Add(_energyStruct, el);
                if (el.Text == "Нефтяная инфраструктура")
                    dicDDM.Add(_neftyStrcut, el);
                if (el.Text == "Тематические карты")
                    dicDDM.Add(_tematicMap, el);
                if (el.Text == "Космические снимки")
                    dicDDM.Add(_cosmoPhoto, el);
            }
            for (int i = 0; i < listCheckBoxes.Count; i++)
            {
                if (listCheckBoxes[i].Text == "Тест")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_test, listCheckBoxes[i - 1]);                  
                }
                if(listCheckBoxes[i].Text =="Новая группа 2")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_newGroup2, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Новая группа")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_newGroup, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Зоя. Тест")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_zoyaTest, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Московская область")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_moscowArea, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Газовая инфраструктура")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_gasStruct, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Энергетическая инфраструктура")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_energyStruct, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Нефтяная инфраструктура")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_neftyStrcut, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Тематические карты")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_tematicMap, listCheckBoxes[i - 1]);
                }
                if (listCheckBoxes[i].Text == "Космические снимки")
                {
                    Thread.Sleep(200);
                    dicCB.Add(_cosmoPhoto, listCheckBoxes[i - 1]);
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
            if (dicDDM[_test].Selected)
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

        private bool getSelectedNewGroup2()
        {
            if (dicDDM[_newGroup2].Selected)
                return true;
            return false;
        }

        /// <summary>
        /// Возвращает значение 'true' если чекбокс слоя 'Новая группа 2' активен.
        /// </summary>
        /// <returns></returns>
        public bool GetSelectedNewGroup2
        {
            get
            {
                return getSelectedNewGroup2();
            }
        }

    
        private bool getSelectedNewGroup()
        {
            if (dicDDM[_newGroup].Selected)
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
            if (dicDDM[_zoyaTest].Selected)
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
            if (dicDDM[_moscowArea].Selected)
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
            if (dicDDM[_gasStruct].Selected)
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
            if (dicDDM[_energyStruct].Selected)
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
            if (dicDDM[_neftyStrcut].Selected)
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
            if (dicDDM[_tematicMap].Selected)
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
            if (dicDDM[_cosmoPhoto].Selected)
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
            dicCB[_test].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers TestOpenCloseList()
        {
            dicDDM[_test].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу слоя 'Новая группа 2'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroup2CheckBoxClick()
        {
            dicCB[_newGroup2].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Новая группа 2'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroup2OpenCloseList()
        {
            dicDDM[_newGroup2].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupCheckBoxClick()
        {
            dicCB[_newGroup].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Новая группа'.
        /// </summary>
        /// <returns></returns>
        public Layers NewGroupOpenCloseList()
        {
            dicDDM[_newGroup].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestCheckBoxClick()
        {
            dicCB[_zoyaTest].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Зоя. Тест'.
        /// </summary>
        /// <returns></returns>
        public Layers ZoyaTestOpenCloseList()
        {
            dicDDM[_zoyaTest].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaCheckBoxClick()
        {
            dicCB[_moscowArea].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Московская область'.
        /// </summary>
        /// <returns></returns>
        public Layers MoscowAreaOpenCloseList()
        {
            dicDDM[_moscowArea].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructCheckBoxClick()
        {
            dicCB[_gasStruct].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Газовая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers GasStructOpenCloseList()
        {
            dicDDM[_gasStruct].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStructCheckBoxClick()
        {
            dicCB[_energyStruct].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Энергетическая инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers EnergyStructOpenCloseList()
        {
            dicDDM[_energyStruct].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructCheckBoxClick()
        {
            dicCB[_neftyStrcut].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел 'Нефтяная инфраструктура'.
        /// </summary>
        /// <returns></returns>
        public Layers NeftyStructOpenCloseList()
        {
            dicDDM[_neftyStrcut].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapCheckBoxClick()
        {
            dicCB[_tematicMap].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Тематические карты'.
        /// </summary>
        /// <returns></returns>
        public Layers TematicMapOpenCloseList()
        {

            dicDDM[_tematicMap].Click();
            return this;
        }

        /// <summary>
        /// Выполняет клик по чекбоксу  слоя 'Космические снимки'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoCheckBoxClick()
        {
            dicCB[_cosmoPhoto].Click();
            return this;
        }

        /// <summary>
        /// Открывает раздел  'Космические снимки.'.
        /// </summary>
        /// <returns></returns>
        public Layers CosmoPhotoOpenCloseList()
        {
            dicDDM[_cosmoPhoto].Click();
            return this;
        }
    }
}
