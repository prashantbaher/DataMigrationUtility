using Microsoft.Office.Interop.Excel;
using ReadExcel.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using excel = Microsoft.Office.Interop.Excel;

namespace ReadExcel.Library
{
    public static class ExcelUtil
    {
        public async static Task<List<OperationItemModel>> ReadExcel(string excelPath)
        {
            var list = new List<OperationItemModel>();
            if (string.IsNullOrEmpty(excelPath)) return null;
            await Task.Run(() =>
              {

                  var item = new OperationItemModel();
                  excel._Application excelApp = null;
                  excel.Workbook workBook = null;
                  excel.Worksheet sheet;
                  try
                  {
                      excelApp = new excel.Application();
                      workBook = excelApp.Workbooks.Open(excelPath, ReadOnly: true);//($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Input\\AE.0010.xlsx", ReadOnly: true);
                      sheet = workBook.Worksheets["StockList"];
                      var usedRange = sheet.UsedRange;
                      var rows = usedRange.Rows;
                      var columns = usedRange.Columns;

                      var column = new Dictionary<int, string>();
                      var count = rows.Count;

                      object[,] valueArray = (object[,])usedRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                      for (int i = 1; i <= columns.Count; i++)
                      {
                          var cellValue = valueArray[1, i];
                          column?.Add(i, cellValue?.ToString());
                      }
                      var sequence = 0;
                      for (int i = 1; i <= count; i++)
                      {
                          if (sequence > 0)
                          {
                              if (!GetExcelValue(nameof(item.StockName), valueArray, column, i)?.Equals("-") ?? false)
                              {
                                  var fileType = GetFileType(valueArray, column, i);
                                  list.Add(new OperationItemModel()
                                  {
                                      RowIndex = sequence,
                                      OperationNo = GetExcelValue(nameof(item.OperationNo), valueArray, column, i),
                                      Z1 = GetExcelValue(nameof(item.Z1), valueArray, column, i),
                                      Z2 = GetExcelValue(nameof(item.Z2), valueArray, column, i),
                                      Z3 = GetExcelValue(nameof(item.Z3), valueArray, column, i),
                                      ZEDrawingNumber = GetExcelValue(nameof(item.ZEDrawingNumber), valueArray, column, i),
                                      FileName = GetExcelValue(nameof(item.FileName), valueArray, column, i),
                                      Symbol = GetExcelValue(nameof(item.Symbol), valueArray, column, i),
                                      AMT = GetExcelValue(nameof(item.AMT), valueArray, column, i),
                                      SheetNumber = GetExcelValue(nameof(item.SheetNumber), valueArray, column, i),
                                      StockName = GetExcelValue(nameof(item.StockName), valueArray, column, i),
                                      SupplierName = GetExcelValue(nameof(item.SupplierName), valueArray, column, i),
                                      SupplierPartNumber = GetExcelValue(nameof(item.SupplierPartNumber), valueArray, column, i),
                                      ItemNumber = GetExcelValue(nameof(item.ItemNumber), valueArray, column, i),
                                      FileIdentificationType = (int)fileType,
                                  });
                              }
                          }
                          sequence++;
                      }
                      string lastfileName = string.Empty;
                      for (int i = 0; i < list.Count; i++)
                      {
                          var row = list[i];
                          if (string.IsNullOrEmpty(row.ItemNumber) || row.ItemNumber.Trim().Equals("-"))
                          {
                              for (int j = i + 1; j < list.Count; j++)
                              {
                                  if (!string.IsNullOrEmpty(list[j].ItemNumber) && !list[j].ItemNumber.Trim().Equals("-"))
                                  {
                                      row.ItemNumber = list[j].ItemNumber;
                                      break;
                                  }
                              }
                          }
                      }
                  }
                  catch (Exception ex)
                  {

                  }
                  finally
                  {
                      sheet = null;
                      workBook?.Close(false, Type.Missing, Type.Missing);
                      Marshal.ReleaseComObject(workBook);
                      excelApp?.Quit();
                      Marshal.FinalReleaseComObject(excelApp);
                  }
              });
            return list;
        }


        public static List<OpDataModel> ReadExcelAndProcess(string excelPath)
        {
            var list = new List<OpDataModel>(); var item = new OpDataModel();
            excel._Application excelApp = null;
            excel.Workbook workBook = null;
            excel.Worksheet sheet;
            try
            {
                excelApp = new excel.Application();
                workBook = excelApp.Workbooks.Open(excelPath, ReadOnly: true);//($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Input\\AE.0010.xlsx", ReadOnly: true);
                sheet = workBook.Worksheets["StockList"];
                var usedRange = sheet.UsedRange;
                var rows = usedRange.Rows;
                var columns = usedRange.Columns;

                var column = new Dictionary<int, string>();
                var count = rows.Count;

                object[,] valueArray = (object[,])usedRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                for (int i = 1; i <= columns.Count; i++)
                {
                    var cellValue = valueArray[1, i];
                    column?.Add(i, cellValue?.ToString());
                }
                var sequence = 0;
                for (int i = 1; i <= count; i++)
                {
                    if (sequence > 0)
                    {
                        if (!GetValue(nameof(item.NameOrStockSize), valueArray, column, i)?.Equals("-") ?? false)
                        {
                            var fileType = GetFileType(valueArray, column, i);
                            list.Add(new OpDataModel()
                            {
                                RowIndex = sequence,
                                OperNumber = GetValue(nameof(item.OperNumber), valueArray, column, i),
                                ZENumber = GetValue(nameof(item.ZENumber), valueArray, column, i),
                                FileName = GetValue(nameof(item.FileName), valueArray, column, i),
                                Symbol = GetValue(nameof(item.Symbol), valueArray, column, i),
                                NameOrStockSize = GetValue(nameof(item.NameOrStockSize), valueArray, column, i),
                                SheetNo = GetValue(nameof(item.SheetNo), valueArray, column, i),
                                ABCDItemNo = GetValue(nameof(item.ABCDItemNo), valueArray, column, i),
                                FileType = fileType,
                            });
                        }
                    }
                    sequence++;
                }
                string lastfileName = string.Empty;
                for (int i = 0; i < list.Count; i++)
                {
                    var row = list[i];
                    if (string.IsNullOrEmpty(row.ABCDItemNo) || row.ABCDItemNo.Trim().Equals("-"))
                    {
                        for (int j = i + 1; j < list.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(list[j].ABCDItemNo) && !list[j].ABCDItemNo.Trim().Equals("-"))
                            {
                                row.ABCDItemNo = list[j].ABCDItemNo;
                                break;
                            }
                        }
                    }
                }
                processDataForSheetNoAndSymbol(list, out List<OpDataModel> OpProcessedItems);
                processDataForHexNo(ref OpProcessedItems);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                sheet = null;
                workBook?.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(workBook);
                excelApp?.Quit();
                Marshal.FinalReleaseComObject(excelApp);
            }
            return list;
        }

        private static void processDataForSheetNoAndSymbol(List<OpDataModel> OpItems, out List<OpDataModel> OpProcessedItems)
        {
            OpProcessedItems = new List<OpDataModel>();

            Dictionary<string, int> OpAndMaxSheetNo = new Dictionary<string, int>();
            Dictionary<string, int> OpAndMaxPartSymbol = new Dictionary<string, int>();
            Dictionary<string, int> OpAndMaxAssemblySymbol = new Dictionary<string, int>();

            var ZeItems = OpItems.OrderBy(_ => _.RowIndex).GroupBy(_ => _.ZENumber);
            foreach (var ze in ZeItems)
            {
                if (!OpAndMaxSheetNo.ContainsKey(ze.Key)) OpAndMaxSheetNo.Add(ze.Key, 0);
                if (!OpAndMaxPartSymbol.ContainsKey(ze.Key)) OpAndMaxPartSymbol.Add(ze.Key, 100);
                if (!OpAndMaxAssemblySymbol.ContainsKey(ze.Key)) OpAndMaxAssemblySymbol.Add(ze.Key, 0);


                //ZE Level Symbol logic
                var allZEs = ze.Where(_ => _.FileType == FileIdentification.ZELevelAssembly).OrderBy(_ => _.RowIndex);
                foreach (var item in allZEs)
                {
                    int maxAsmSymbol = OpAndMaxAssemblySymbol[ze.Key] + 1;
                    item.NewSymbol = maxAsmSymbol.ToString().PadLeft(3, '0');
                    OpAndMaxAssemblySymbol[ze.Key] = maxAsmSymbol;
                }
                foreach (var item in ze)
                {
                    if (item.FileType == FileIdentification.ZELevelAssembly)
                    {
                        OpProcessedItems.Add(item);
                        continue;
                    }
                    var refItem = OpProcessedItems.FirstOrDefault(_ => _.FileType == item.FileType && _.ABCDItemNo == item.ABCDItemNo);
                    if (refItem != null)
                    {
                        item.NewSheetNo = refItem.SheetNo;
                        item.RefSheetNo = refItem.SheetNo;
                        item.RefZENumber = refItem.ZENumber;
                        item.RefSymbol = refItem.Symbol;
                        OpProcessedItems.Add(item);
                    }
                    else
                    {
                        int maxSheetNo = OpAndMaxSheetNo[ze.Key] + 1;
                        item.NewSheetNo = maxSheetNo.ToString().PadLeft(3, '0');

                        if (item.FileType == FileIdentification.Part)
                        {
                            int maxPartSymbol = OpAndMaxPartSymbol[ze.Key] + 1;
                            item.NewSymbol = maxPartSymbol.ToString().PadLeft(3, '0');
                            OpAndMaxPartSymbol[ze.Key] = maxPartSymbol;
                        }

                        if (item.FileType == FileIdentification.Assembly)
                        {
                            if (!item.FileName.Equals(item.ABCDItemNo))
                            {
                                int maxAsmSymbol = OpAndMaxAssemblySymbol[ze.Key] + 1;
                                item.NewSymbol = maxAsmSymbol.ToString().PadLeft(3, '0');
                                OpAndMaxAssemblySymbol[ze.Key] = maxAsmSymbol;
                            }
                        }

                        OpProcessedItems.Add(item);
                        OpAndMaxSheetNo[ze.Key] = maxSheetNo;
                    }
                }
            }
        }

        private static void processDataForHexNo(ref List<OpDataModel> list)
        {
            var zeRefPartsGroup = list.Where(_ => !string.IsNullOrEmpty(_.RefZENumber) && _.FileType == FileIdentification.Part && _.ZENumber != _.RefZENumber)
                .OrderBy(_ => _.RowIndex)
                .GroupBy(_ => _.ZENumber.Trim());
            foreach (var ze in zeRefPartsGroup)
            {
                var refParts = ze
                    .OrderBy(_ => _.RowIndex)
                    .GroupBy(_ => _.ZENumber);

                foreach (var item in refParts)
                {
                    int hxNo = 0;
                    var refZeGroups = item.OrderBy(_ => _.RowIndex).GroupBy(_ => _.RefZENumber.Trim());
                    foreach (var refZeGroup in refZeGroups)
                    {
                        hxNo++;
                        foreach (var part in refZeGroup)
                        {
                            part.HEXNo = $"{hxNo}H";
                        }
                    }
                }
            }
        }

        private static string GetValue(string propName, object[,] valueArray, Dictionary<int, string> columns, int rowIndex)
        {
            int index = -1;
            string value = string.Empty;
            switch (propName)
            {
                case "OperNumber":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("OPER NO.") ?? false).Key;
                    break;
                case "FileName":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("File name") ?? false).Key;
                    break;
                case "ZENumber":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("ZE DRAWING NUMBER") ?? false).Key;
                    break;
                case "Symbol":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("SYM.") ?? false).Key;
                    break;
                case "SheetNo":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("SHT.") ?? false).Key;
                    break;
                case "NameOrStockSize":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("NAME OR STOCK SIZE") ?? false).Key;
                    break;
                case "ABCDItemNo":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("ABCD ITEM NO") ?? false).Key;
                    break;
                default:
                    break;
            }
            if (index >= 0)
            {
                value = valueArray[rowIndex, index]?.ToString().Trim();
            }
            return value;
        }

        private static FileIdentification GetFileType(object[,] valueArray, Dictionary<int, string> column, int rowIndex)
        {
            var item = new OperationItemModel();
            var fileType = FileIdentification.Part;
            var nameOrStockSize = nameof(item.StockName);

            if (GetExcelValue(nameof(item.SheetNumber), valueArray, column, rowIndex)?.Trim().Equals("-") ?? false) fileType = FileIdentification.ZELevelAssembly;
            else if (GetExcelValue(nameOrStockSize, valueArray, column, rowIndex)?.Contains("STOCKLIST") ?? false) fileType = FileIdentification.StockList;
            else if (GetExcelValue(nameOrStockSize, valueArray, column, rowIndex)?.Contains("DRAWING") ?? false) fileType = FileIdentification.Drawing;
            else
            {
                var sym = GetExcelValue(nameof(item.Symbol), valueArray, column, rowIndex);
                int.TryParse(sym, out int intSym);
                if (intSym > 100)
                    fileType = FileIdentification.Part;
                else
                    fileType = FileIdentification.Assembly;
            }
            return fileType;
        }


        private static string GetExcelValue(string propName, object[,] valueArray, Dictionary<int, string> columns, int rowIndex)
        {
            int index = -1;
            string value = string.Empty;
            switch (propName)
            {
                case "OperationNo":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("OPER NO.") ?? false).Key;
                    break;
                case "Z1":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("Z1") ?? false).Key;
                    break;
                case "Z2":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("Z2") ?? false).Key;
                    break;
                case "Z3":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("Z3") ?? false).Key;
                    break;
                case "FileName":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("File name") ?? false).Key;
                    break;
                case "ZEDrawingNumber":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("ZE DRAWING NUMBER") ?? false).Key;
                    break;
                case "Symbol":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("SYM.") ?? false).Key;
                    break;
                case "AMT":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("AMT.") ?? false).Key;
                    break;
                case "SheetNumber":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("SHT.") ?? false).Key;
                    break;
                case "StockName":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("NAME OR STOCK SIZE") ?? false).Key;
                    break;
                case "SupplierName":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("STANDARD/SUPPLIER NAME") ?? false).Key;
                    break;
                case "SupplierPartNumber":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("STANDARD/SUPPLIER PART NUMBER") ?? false).Key;
                    break;
                case "ItemNumber":
                    index = columns.FirstOrDefault(_ => _.Value?.Equals("ABCD ITEM NO") ?? false).Key;
                    break;
                default:
                    break;
            }
            if (index >= 0)
            {
                value = valueArray[rowIndex, index]?.ToString().Trim();
            }
            return value;
        }


    }
}
