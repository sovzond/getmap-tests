using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.IO;

namespace GetMapTest
{
    /// <summary>
    /// Тестирует инструмент 'Выборка'.
    /// </summary>
    [TestClass]
    public class TestSelection
    {
        private IWebDriver driver;
        private bool assertForExportToExcel;
        private const int countObjectsPartialLine = 2;
        private const int countObjectsPratialSquare = 2;
        private const int countObjectsPartialPoligon = 3;
        private const int countObjectsPartialBuffer = 2;
        private const int countObjectsFull = 23;
        private const string _gpzpoint = "ГПЗ (точка)";
        private const string _gpzpoligon = "ГПЗ (полигон)";
        private const string _gazoprovod = "Газопровод";
        private const string _leftFullArea = "leftFullArea";
        private const string _rightFullArea = "rightFullArea";
        private const string _gpzpointXls = "ГПЗ (точка).xls";
        private const string _gpzpoligonXls = "ГПЗ (полигон).xls";
        private const string _gazoprovodXls = "Газопровод.xls";
        private const string _leftPartialSelectionSquare = "leftPartialSelectionSquare";
        private const string _rightPartialSelectionSquare = "rightPartialSelectionSquare";
        private const string _leftPartialSelectionPoligon = "leftPartialSelectionPoligon";
        private const string _rightPartialSelectionPoligon = "rightPartialSelectionPoligon";
        private const string _bottomPartialSelectionPoligon = "bottomPartialSelectionPoligon";
        private const string _bottomPartialSelectionLine = "bottomPartialSelectionLine";
        private const string _upPartialSelectionLine = "upPartialSelectionLine";
        private const string _upPartialSelectionBuffer = "upPartialSelectionBuffer";
        private const string _bottomPartialSelectionBuffer = "bottomPartialSelectionBuffer";
        private const string locationTextInResTable = "#nothing";
        private const string locationElementsForMove = "#map";
        private const string locationHeaderResTable = "#resultDiv";
        private const string locationResultTable = "table.resultTable";
        private const string locationCountRow = "table.resultTable tr.objectRow";
        private const string locationCountRowOpened = "table.resultTable caption.layerNameOpened";
        private const string locationCountRowObjects = "table.resultTable tr.objectRow > td";
        private const string locationButtonsExportToExcel = "a.excel";
        private const string extentGPZPoligon = "7779587.7387138,8563218.0409028,7782993.9637862,8564639.2918972";
        private const string extentGPZPoint = "7790786.6785935,8571267.4096605,7794192.9036659,8572688.6606549";
        private const string extentGazoprovod = "7311214.731272,8298708.0082566,8183208.349828,8662548.2628434";
        private const string locationButtonsInSelection = "div.svzDropMenuButtonMenu > div";
        private const string locationButtonSquareActive = "div.svzDropMenuButtonMenu > div.selectSquareActive";
        private const string locationButtonPoligonActive = "div.svzDropMenuButtonMenu > div.editorAddPoligonActive";
        private const string locationButtonLineActive = "div.svzDropMenuButtonMenu > div.editorAddLineActive";
        private const string locationButtonBufferActive = "div.svzDropMenuButtonMenu > div.selectBufferActive";
        private const string locationButtonCancelActive = "svzSimpleButton editorStop svzSimpleButtonActive";
        private const string attributeStyletTurOff = "display: block; height: 20px;";
        private const string attributeStyleClose = "display: none;";
        private int[] splitedPixelsCenter;
        private Actions builder;
        private IWebElement elementForMove;
        private IWebElement elementTextInResTable;
        private IWebElement elementResTable;
        private Dictionary<string, IWebElement> dicLayers;
        private Dictionary<string, int[]> dicListPixels;
        private  List<string> listForDI;
        private IList<IWebElement> listCountRowObjects;
        private IList<IWebElement> listCountRow;
        private IList<IWebElement> listCountRowOpened;
        private IList<IWebElement> listResTable;
        private Utils.TransformJS jsTransform;

        [TestInitialize]
        public void Setup()
        {
            driver = Settings.Instance.createDriver();
            GUI.Login.get(driver, Settings.Instance.BaseUrl).loginAsGuest();
            Assert.AreEqual(Settings.Instance.BaseUrl, driver.Url, "Не удалось пройти авторизацию");
            jsTransform = new Utils.TransformJS(driver);
            builder = new Actions(driver);
            elementForMove = driver.FindElement(By.CssSelector(locationElementsForMove));
            dicLayers = new Dictionary<string, IWebElement>();
            dicListPixels = new Dictionary<string, int[]>();
        }

        /// <summary>
        /// Проверяет выделение всех объектов группы слоев 'Газовая инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckFullSelection()
        {
            SelectFullArea();
            AssertOnPartialSelection(countObjectsFull);
        }

        /// <summary>
        /// Выполняет проверку на альтернативный вариант,а то есть
        ///  проверяет случайный клик по карте и
        ///   выделение области, в которой отсутствуют объекты группы слоев 'Газовая инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckAlternativeOptions()
        {
            RandomClickOnMap();
            AreaIsNull();

        }

        /// <summary>
        /// Проверяет частисное выделение объектов группы слоев 'Газовая инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckPartialSelection()
        {
            PartialSelectionSquare();
            PartialSelectionLine();
            PartialSelectionPoligon();
            PartialSelectionBuffer();
        }

        /// <summary>
        /// Проверяет приближение к слою каждого слоя группы слоев 'Газовая инфраструктура'.
        /// </summary>
        [TestMethod]
        public void CheckZoomToLayer()
        {
            ZoomToGPZPoligon();
            ZoomToGPZPoint();
            ZoomToGazoprovod();
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопок 'Свернуть' и  'Закрыть' результирующей таблицы поиска.
        /// </summary>
        [TestMethod]
        public void CheckTurnOffAndCloseRusTable()
        {
            SelectFullArea();
            elementResTable = driver.FindElement(By.CssSelector(locationHeaderResTable));
            System.Threading.Thread.Sleep(1000);
            GUI.BottomMenu.get(driver).TurnOnOff();
            Assert.AreEqual(attributeStyletTurOff, elementResTable.GetAttribute("style"), "После клика по кнопке 'Свернуть' " +
                "результирующей таблице поиска, таблица не свернулась.");
            System.Threading.Thread.Sleep(1000);
            GUI.BottomMenu.get(driver).TurnOnOff();
            System.Threading.Thread.Sleep(1000);
            GUI.BottomMenu.get(driver).Close();
            Assert.AreEqual(attributeStyleClose, elementResTable.GetAttribute("style"), "После клика по кнопке 'Закрыть' " +
                "результирующей таблице, таблица не закрылась.");
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопки 'Отмена' инструмента 'Выборка'.
        /// </summary>
        [TestMethod]
        public void CheckCancelSelection()
        {
            SelectFullArea();
            GUI.BottomMenu.get(driver).Close();
            GUI.MenuNavigation.get(driver).SelectionButton().SelectionButtonCancel();
            List<string> listActiveButtons = new List<string>();
            IList<IWebElement> listButtons = driver.FindElements(By.CssSelector(locationButtonsInSelection));
            foreach (var r in listButtons)
            {
                if (r.GetAttribute("class").EndsWith("Active")
                    && r.GetAttribute("class") != locationButtonCancelActive)
                    listActiveButtons.Add(r.GetAttribute("class"));
            }
            Assert.AreEqual(0, listActiveButtons.Count, "После клика по кнопке 'Отмена' инстурмента 'Выборка' , "
                  + "выделение не было отменено.");
        }

        /// <summary>
        /// Выполняет проверку на корректную работу кнопки 'Экспорт в excel'.
        /// </summary>
        [TestMethod]
        public void CheckExportToExcel()
        {
            assertForExportToExcel = false;
            listForDI = new List<string>();
            SelectFullArea();
            IList<IWebElement> listButtonsExportToExcel = driver.FindElements(By.CssSelector(locationButtonsExportToExcel));
            for(int i=0;i<listButtonsExportToExcel.Count;i++)
            {
                listButtonsExportToExcel[i].Click();
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloads = path + "\\Downloads";
            DirectoryInfo di = new DirectoryInfo(downloads);
            FileInfo[] fi = di.GetFiles();
            foreach (var r in fi)
                listForDI.Add(r.ToString());
            AssertExportToExcel(_gazoprovodXls);
            AssertExportToExcel(_gpzpointXls);
            AssertExportToExcel(_gpzpoligonXls);
        }

        private void AssertExportToExcel(string filename)
        {
            for (int i = 0; i < listForDI.Count; i++)
            {
                if (listForDI[i] == filename)
                    assertForExportToExcel = true;
            }
            Assert.IsTrue(assertForExportToExcel, "После клика по кноке экспорт в эксель, файл " + filename + " не скачался.");
            assertForExportToExcel = false;
        }

        [TestCleanup]
        public void Clean()
        {
            GUI.Cleanup.get(driver).Quit();
        }

        private void SelectFullArea()
        {
            DataPreparation(4);
            GUI.MenuNavigation.get(driver).SelectionButton();
            int pointRightBottomX = dicListPixels[_rightFullArea][0] - dicListPixels[_leftFullArea][0];
            int pointRightBottomY = dicListPixels[_rightFullArea][1] - dicListPixels[_leftFullArea][1];
            builder.MoveToElement(elementForMove, dicListPixels[_leftFullArea][0], dicListPixels[_leftFullArea][1]).ClickAndHold()
                .MoveByOffset(pointRightBottomX,pointRightBottomY).Release().Perform();
            System.Threading.Thread.Sleep(5000);
        }

        private void RandomClickOnMap()
        {
            DataPreparation(3);
            splitedPixelsCenter = jsTransform.ConvertToSplitedPixelsFromLonLat(jsTransform.GetMapCenterString());
            GUI.MenuNavigation.get(driver).SelectionButton();
            builder.MoveToElement(elementForMove, splitedPixelsCenter[0] + 50, splitedPixelsCenter[1]).Click().Perform();
            AssertMessageIsNull();
        }

        private void AreaIsNull()
        {
            GUI.BottomMenu.get(driver).Close();
            builder.MoveToElement(elementForMove, splitedPixelsCenter[0] + 50, splitedPixelsCenter[1]).ClickAndHold()
                .MoveByOffset(100, 100).Release().Perform();
            AssertMessageIsNull();
        }

        private void PartialSelectionSquare()
        {
            DataPreparation(3);
            GUI.MenuNavigation.get(driver).SelectionButton();
            int pointRightBottomX = dicListPixels[_rightPartialSelectionSquare][0] - dicListPixels[_leftPartialSelectionSquare][0];
            int pointRightBottomY = dicListPixels[_rightPartialSelectionSquare][1] - dicListPixels[_leftPartialSelectionSquare][1];
            builder.MoveToElement(elementForMove, dicListPixels[_leftPartialSelectionSquare][0], dicListPixels[_leftPartialSelectionSquare][1]).ClickAndHold()
                .MoveByOffset(pointRightBottomX, pointRightBottomY).Release().Perform();
            AssertOnPartialSelection(countObjectsPratialSquare);
            GUI.BottomMenu.get(driver).Close();
        }

        private void PartialSelectionPoligon()
        {
            GUI.MenuNavigation.get(driver).SelectionButton().SelectionButtonPoligon();
            int distanceToLeftX = dicListPixels[_leftPartialSelectionPoligon][0] - dicListPixels[_bottomPartialSelectionPoligon][0];
            int distanceToLeftY = dicListPixels[_leftPartialSelectionPoligon][1] - dicListPixels[_bottomPartialSelectionPoligon][1];
            int distanceToRightX = dicListPixels[_rightPartialSelectionPoligon][0] - dicListPixels[_leftPartialSelectionPoligon][0];
            int distanceToRightY = dicListPixels[_rightPartialSelectionPoligon][1] - dicListPixels[_leftPartialSelectionPoligon][1];
            builder.MoveToElement(elementForMove, dicListPixels[_bottomPartialSelectionPoligon][0], dicListPixels[_bottomPartialSelectionPoligon][1]).Click()
                .MoveByOffset(distanceToLeftX, distanceToLeftY).Click()
                .MoveByOffset(distanceToRightX, distanceToRightY).DoubleClick().DoubleClick().Perform();
            AssertOnPartialSelection(countObjectsPartialPoligon);
            GUI.BottomMenu.get(driver).Close();
        }

        private void PartialSelectionLine()
        {
            GUI.MenuNavigation.get(driver).SelectionButton().SelectionButtonLine();
            int distanceToLeftX = dicListPixels[_bottomPartialSelectionLine][0] - dicListPixels[_upPartialSelectionLine][0];
            int distanceToLeftY = dicListPixels[_bottomPartialSelectionLine][1] - dicListPixels[_upPartialSelectionLine][1];
            builder.MoveToElement(elementForMove, dicListPixels[_upPartialSelectionLine][0], dicListPixels[_upPartialSelectionLine][1]).Click().MoveByOffset(distanceToLeftX, distanceToLeftY)
                .DoubleClick().DoubleClick().Perform();
            AssertOnPartialSelection(countObjectsPartialLine);
            GUI.BottomMenu.get(driver).Close();
        }

        private void PartialSelectionBuffer()
        {
            GUI.MenuNavigation.get(driver).SelectionButton().SelectionButtonBuffer();
            int distanceToBottomX = dicListPixels[_bottomPartialSelectionBuffer][0] - dicListPixels[_upPartialSelectionBuffer][0];
            int distanceToBottomY = dicListPixels[_bottomPartialSelectionBuffer][1] - dicListPixels[_upPartialSelectionBuffer][1];
            builder.MoveToElement(elementForMove, dicListPixels[_upPartialSelectionBuffer][0], dicListPixels[_upPartialSelectionBuffer][1]).ClickAndHold()
                .MoveByOffset(distanceToBottomX, distanceToBottomY).Release().Perform();
            AssertOnPartialSelection(countObjectsPartialBuffer);
        }

        private void ZoomToGPZPoligon()
        {
            SelectFullArea();
            listResTable = driver.FindElements(By.CssSelector(locationResultTable));
            for (int i = 0; i < listResTable.Count; i++)
            {
                if (listResTable[i].Text == "ГПЗ (полигон)")
                    dicLayers.Add(_gpzpoligon, listResTable[i]);
            }
            dicLayers[_gpzpoligon].Click();
            ClickOnObject(0);
            Assert.AreEqual(extentGPZPoligon, jsTransform.GetCurrentExtent(), "После приблежения к объекту слоя 'ГПЗ(полигон)' группы слоев "
                + "'Газовая инфраструктура, приближение не было осуществлено.' ");
            System.Threading.Thread.Sleep(2000);
            CloseOpenedRow();
        }

        private void ZoomToGPZPoint()
        {
            for (int i = 0; i < listResTable.Count; i++)
            {
                if (listResTable[i].Text == "ГПЗ (точка)")
                    dicLayers.Add(_gpzpoint, listResTable[i]);
            }
            dicLayers[_gpzpoint].Click();
            ClickOnObject(2);
            Assert.AreEqual(extentGPZPoint, jsTransform.GetCurrentExtent(), "После приблежения к объекту слоя 'ГПЗ(точка)' группы слоев "
                + "'Газовая инфраструктура, приближение не было осуществлено.' ");
            System.Threading.Thread.Sleep(2000);
            CloseOpenedRow();
        }

        private void ZoomToGazoprovod()
        {
            for (int i = 0; i < listResTable.Count; i++)
            {
                if (listResTable[i].Text == "Газопровод")
                    dicLayers.Add(_gazoprovod, listResTable[i]);
            }
            dicLayers[_gazoprovod].Click();
            ClickOnObject(0);
            Assert.AreEqual(extentGazoprovod, jsTransform.GetCurrentExtent(), "После приблежения к объекту слоя 'Газопровод' группы слоев "
                + "'Газовая инфраструктура, приближение не было осуществлено.' ");
        }

        private void DataPreparation(int count)
        {
            GUI.SlideMenu.get(driver).OpenLayers();
            System.Threading.Thread.Sleep(1000);
            GUI.SlideMenu.get(driver).OpenBaseLayers().OpenGoogle();
            if (!GUI.Layers.get(driver).GetSelectedGasStruct)
                GUI.Layers.get(driver).GasStructCheckBoxClick();
            GUI.InputCoordWnd.get(driver).setLon(60, 29, 26).setLat(71, 6, 9).click();
            for (int i = 0; i < count; i++)
                GUI.ScaleMenu.get(driver).DecrementButton();
            System.Threading.Thread.Sleep(500);
            int[] splitedPixelsCenter = jsTransform.ConvertToSplitedPixelsFromLonLat(jsTransform.GetMapCenterString());
            builder.MoveToElement(elementForMove, Convert.ToInt32(splitedPixelsCenter[0]),
                Convert.ToInt32(splitedPixelsCenter[1])).ClickAndHold().MoveByOffset(10, 10).Release().Perform();
            System.Threading.Thread.Sleep(500);
            builder.MoveToElement(elementForMove, Convert.ToInt32(splitedPixelsCenter[0] - 20),
                Convert.ToInt32(splitedPixelsCenter[1] - 20)).ClickAndHold().MoveByOffset(10, 10).Release().Perform();
            dicListPixels.Add(_upPartialSelectionBuffer, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7830759", "8623855")));
            dicListPixels.Add(_bottomPartialSelectionBuffer, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7830759", "8592057")));
            dicListPixels.Add(_bottomPartialSelectionLine, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7783659", "8544660")));
            dicListPixels.Add(_upPartialSelectionLine, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7778156", "8583496")));
            dicListPixels.Add(_bottomPartialSelectionPoligon, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7813632", "8557813")));
            dicListPixels.Add(_leftPartialSelectionPoligon, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7776933", "8580439")));
            dicListPixels.Add(_rightPartialSelectionPoligon, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7814134", "8580439")));
            dicListPixels.Add(_leftPartialSelectionSquare, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7802393", "8630598")));
            dicListPixels.Add(_rightPartialSelectionSquare, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7864966", "8606138")));
            dicListPixels.Add(_leftFullArea, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("7633842", "8643117")));
            dicListPixels.Add(_rightFullArea, jsTransform.SplitPixels(jsTransform.GetPixelsFromLonLat("8049660", "8381397")));
        }

        private void AssertMessageIsNull()
        {
            System.Threading.Thread.Sleep(5000);
            elementTextInResTable = driver.FindElement(By.CssSelector(locationTextInResTable));
            Assert.AreEqual("Ничего не найдено", elementTextInResTable.Text, "После случайного клика по карте в область" +
                 " отсутствующих тематических слоев или же выделение пустой области"
               + " не отобразилась таблица с текстом 'Ничего не найдено'.");
        }

        private int GetCountRow()
        {
            listCountRow = driver.FindElements(By.CssSelector(locationCountRow));
            int count = listCountRow.Count;
            return count;
        }

        private void AssertOnPartialSelection(int countObjects)
        {
            System.Threading.Thread.Sleep(5000);
            listResTable = driver.FindElements(By.CssSelector(locationResultTable));
            for (int i = 0; i < listResTable.Count; i++)
            {
                if (listResTable[i].Text == "ГПЗ (точка)")
                    listResTable[i].Click();
                if (listResTable[i].Text == "Газопровод")
                    listResTable[i].Click();
                if (listResTable[i].Text == "ГПЗ (полигон)")
                    listResTable[i].Click();
            }
            Assert.AreEqual(countObjects, GetCountRow(), "После частичного выделения " + countObjects +
                 " объектов слоев группы 'Газовая инфраструктура', не все объекты отобразились в результирующей таблице.");
        }

        private void ClickOnObject(int numberObject)
        {
            listCountRowObjects = driver.FindElements(By.CssSelector(locationCountRowObjects));
            if (numberObject > listCountRowObjects.Count)
                throw new Exception("Вы ввели не корректный номер объекта слоя.");
            listCountRowObjects[numberObject].Click();
        }

        private void CloseOpenedRow()
        {
            listCountRowOpened = driver.FindElements(By.CssSelector(locationCountRowOpened));
            listCountRowOpened[0].Click();
        }

 
    }

}

/*
   private void CheckGasPoligonFullSelection()
        {
            GUI.MenuNavigation.get(driver).SelectionButton();
            GUI.MenuNavigation.get(driver).SelectionButtonPoligon();
            string lonlatLeftUp = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(7950597,8759913)).toString()");
            int[] splitedPixelsUp = SplitPixels(lonlatLeftUp);
            string lonlatLeft = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getPixelFromLonLat(new OpenLayers.LonLat(7575139,8498192)).toString()");
            int[] splitedPixelsLeft = SplitPixels(lonlatLeft);
            string lonlatCenter = (string)jsExecutor.ExecuteScript("return window.portal.stdmap.map.getCenter().toString()");
            int[] splitedPixelsCenter = ConvertToSplitedPixels(lonlatCenter);
            int distanceForUpX = splitedPixelsUp[0] - splitedPixelsCenter[0];
            int distanceForUpY = splitedPixelsUp[1] - splitedPixelsCenter[1];
            int disnatceForLeftX = splitedPixelsLeft[0] - splitedPixelsUp[0];
            int distanceForLeftY = splitedPixelsLeft[1] - splitedPixelsUp[1];
            builder.Click(elementForMove).MoveByOffset(distanceForUpX, distanceForUpY).Click()
                .MoveByOffset(disnatceForLeftX, distanceForLeftY).DoubleClick().DoubleClick().Perform();
            System.Threading.Thread.Sleep(5000);
            PreparationResTable();
            Assert.AreEqual(countObjects, GetCountRow(), "После выделение всех объектов слоев группы 'Газовая инфраструктура', не все объекты отобразились в результирующей таблице.");
        }

    */
