using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel.Library.Models
{
    public class OpDataModel
    {
        public int RowIndex { get; set; }
        //Display["OPER NO"]
        public string OperNumber { get; set; }
        //Display["ZE DRAWING NUMBER"]
        public string ZENumber { get; set; }
        //Display["File name"]
        public string FileName { get; set; }
        //Display["SYM"]
        public string Symbol { get; set; }
        //Display["AMT."]
        public string Amt { get; set; }
        //Display["SHT."]
        public string SheetNo { get; set; }
        //Display["NAME OR STOCK SIZE"]
        public string NameOrStockSize { get; set; }
        //Display["ABCD ITEM NO"]
        public string ABCDItemNo { get; set; }
        //Display["Ref ZE Number"]
        public string RefZENumber { get; set; }
        //Display["Ref SYM."]
        public string RefSymbol { get; set; }
        //Display["HEX No."]
        public string HEXNo { get; set; }
        //Display["Ref SHT."]
        public string RefSheetNo { get; set; }

        public int Level { get; set; }

        public FileIdentification FileType { get; set; }


        //Display["SYM"]
        public string NewSymbol { get; set; }
        //Display["SHT."]
        public string NewSheetNo { get; set; }
    }

    public enum FileIdentification
    {
        ZELevelAssembly,
        StockList,
        Drawing,
        Assembly,
        Part
    }

    public class ZEAssembly
    {
        public int Sequence { get; set; } = 0;
        public string ZENumber { get; set; }
        public List<OpDataModel> Rows { get; set; }
        public int LastPartSymbol { get; set; } = 100;
        public int LastAssemblySymbol { get; set; } = 0;
        public int LastHexNo { get; set; }

    }

    public class ExcelInput
    {
        List<ZEAssembly> AssemblyList { get; set; }
        public int Sequence { get; set; } = 0;
    }
}
