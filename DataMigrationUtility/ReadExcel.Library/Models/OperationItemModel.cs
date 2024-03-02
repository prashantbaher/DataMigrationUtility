namespace ReadExcel.Library.Models
{
    public class OperationItemModel
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public string OperationNo { get; set; }
        public string Z1 { get; set; }
        public string Z2 { get; set; }
        public string Z3 { get; set; }
        public string ZEDrawingNumber { get; set; }
        public string FileName { get; set; }
        public string Symbol { get; set; }
        public string AMT { get; set; }
        public string SheetNumber { get; set; }
        public string StockName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPartNumber { get; set; }
        public string ItemNumber { get; set; }
        public string ReferanceZENumber { get; set; }
        public string ReferanceSymbol { get; set; }
        public string ReferanceSheetNumber { get; set; }
        public int FileIdentificationType { get; set; }
        public string HEXNumber { get; set; }
        public string NewSymbol { get; set; }
        public string NewSheetNumber { get; set; }
        public int RowIndex { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public bool IsProcessed { get; set; }
        public bool Status1 { get; set; }
        public bool Status2 { get; set; }
        public bool Status3 { get; set; }
        public bool Status4 { get; set; }
        public bool Status5 { get; set; }
    }
}
