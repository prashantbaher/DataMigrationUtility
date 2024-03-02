using Caliburn.Micro;
using DataMigrationUtility.EventModels.Company;
using DataMigrationUtility.EventModels.Line;
using DataMigrationUtility.EventModels.LineOperation;
using DataMigrationUtility.EventModels.Plant;
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Enums;
using DMUDataManager.Library.Models;
using ReadExcel.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DataMigrationUtility.ViewModels
{
    public class ProcessOperationItemViewModel : Screen
    {
        public IEventAggregator Events { get; }
        public IOperationItemData OperationItemData { get; }
        public ICompanyModel CompanyModel { get; set; }
        public IPlantModel PlantModel { get; set; }
        public AssemblyLineModel LineModel { get; set; }
        public LineOperationModel OperationModel { get; set; }
        public ProcessOperationItemViewModel(IEventAggregator events, IOperationItemData operationItemData)
        {
            Events = events;
            OperationItemData = operationItemData;
        }

        #region Readonly Properties
        public string CompanyName
        {
            get { return CompanyModel.CompanyName; }
        }
        public string PlantName
        {
            get { return PlantModel.PlantName; }
            set
            {
                PlantModel.PlantName = value;
                NotifyOfPropertyChange(() => PlantName);
            }
        }
        public string LineName
        {
            get { return LineModel.LineName; }
            set
            {
                LineModel.LineName = value;
                NotifyOfPropertyChange(() => LineName);
            }
        }
        public string OperationName
        {
            get { return OperationModel.OperationName; }
            set
            {
                OperationModel.OperationName = value;
                NotifyOfPropertyChange(() => OperationName);
            }
        }
        #endregion

        #region Properties
        private string _ExcelFilePath;
        public string ExcelFilePath
        {
            get => _ExcelFilePath;
            set
            {
                _ExcelFilePath = value;
                NotifyOfPropertyChange(() => ExcelFilePath);
                NotifyOfPropertyChange(() => CanReadExcel);
            }
        }

        private ObservableCollection<OperationItemModel> operationItems;

        public ObservableCollection<OperationItemModel> OperationItems
        {
            get => operationItems;
            set
            {
                operationItems = value;
                NotifyOfPropertyChange(nameof(OperationItems));
                NotifyOfPropertyChange(nameof(CanProcess));
            }
        }


        #endregion

        #region Actions
        private bool _canBack = true;
        public bool CanBack
        {
            get => _canBack;
            set
            {
                _canBack = value;
                NotifyOfPropertyChange(() => _canBack);
            }
        }
        public void Back()
        {
            Console.WriteLine("Back from Process Opreation!");
            Events.PublishOnUIThreadAsync(new ProcessOperationEventModel(Enums.FormActions.Close,
                CompanyModel, PlantModel, LineModel, OperationModel));
        }
        private bool _canBrowseExcel = true;
        public bool CanBrowseExcel
        {
            get
            {
                return _canBrowseExcel;
            }
            set
            {
                _canBrowseExcel = value;
                NotifyOfPropertyChange(() => _canBrowseExcel);
            }
        }

        public void BrowseExcel()
        {
            Console.WriteLine("Browse Excel file!");
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog()
            {
                Filter = "Excel file|*.xlsx"
            };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExcelFilePath = dialog.FileName;
            }
        }

        private bool _canReadExcel = false;
        public bool CanReadExcel
        {
            get
            {
                _canReadExcel = !string.IsNullOrEmpty(ExcelFilePath);
                return _canReadExcel;
            }
            set
            {
                _canReadExcel = value;
                NotifyOfPropertyChange(() => _canReadExcel);
            }
        }
        public async Task ReadExcel()
        {
            Console.WriteLine("Read Excel file!");
            OperationItems = new ObservableCollection<OperationItemModel>();
            var excelData = await ExcelUtil.ReadExcel(ExcelFilePath);
            foreach (var item in excelData.OrderBy(_ => _.RowIndex))
            {
                OperationItemModel itemModel = new OperationItemModel()
                {
                    Id = item.Id,
                    OperationId = item.OperationId,
                    Z1 = item.Z1,
                    Z2 = item.Z2,
                    Z3 = item.Z3,
                    ZEDrawingNumber = item.ZEDrawingNumber,
                    FileName = item.FileName,
                    Symbol = item.Symbol,
                    AMT = item.AMT,
                    SheetNumber = item.SheetNumber,
                    StockName = item.StockName,
                    SupplierName = item.SupplierName,
                    SupplierPartNumber = item.SupplierPartNumber,
                    ItemNumber = item.ItemNumber,
                    FileIdentificationType = (FileIdentificationType)item.FileIdentificationType,
                    RowIndex = item.RowIndex,
                };
                OperationItems.Add(itemModel);
                NotifyOfPropertyChange(nameof(CanProcess));
            }
        }
        private bool _CanProcess = false;
        public bool CanProcess
        {
            get
            {
                _CanProcess = OperationItems?.Count > 0;
                return _CanProcess;
            }
            set
            {
                _CanProcess = value;
                NotifyOfPropertyChange(() => _CanProcess);
            }
        }
        public async Task Process()
        {
            processDataForSheetNoAndSymbol(OperationItems, out ObservableCollection<OperationItemModel> processedData);
            processDataForHexNo(ref processedData);
            OperationItems = processedData;
        }
        #endregion

        #region BL
        private void processDataForSheetNoAndSymbol(ObservableCollection<OperationItemModel> OpItems, out ObservableCollection<OperationItemModel> OpProcessedItems)
        {
            OpProcessedItems = new ObservableCollection<OperationItemModel>();
            Dictionary<string, int> OpAndMaxSheetNo = new Dictionary<string, int>();
            Dictionary<string, int> OpAndMaxPartSymbol = new Dictionary<string, int>();
            Dictionary<string, int> OpAndMaxAssemblySymbol = new Dictionary<string, int>();

            var ZeItems = OpItems.OrderBy(_ => _.RowIndex).GroupBy(_ => _.ZEDrawingNumber);
            foreach (var ze in ZeItems)
            {
                if (!OpAndMaxSheetNo.ContainsKey(ze.Key)) OpAndMaxSheetNo.Add(ze.Key, 0);
                if (!OpAndMaxPartSymbol.ContainsKey(ze.Key)) OpAndMaxPartSymbol.Add(ze.Key, 100);
                if (!OpAndMaxAssemblySymbol.ContainsKey(ze.Key)) OpAndMaxAssemblySymbol.Add(ze.Key, 0);


                //ZE Level Symbol logic
                var allZEs = ze.Where(_ => _.FileIdentificationType == FileIdentificationType.ZELevelAssembly).OrderBy(_ => _.RowIndex);
                foreach (var item in allZEs)
                {
                    int maxAsmSymbol = OpAndMaxAssemblySymbol[ze.Key] + 1;
                    item.NewSymbol = maxAsmSymbol.ToString().PadLeft(3, '0');
                    item.ReferanceZENumber = item.StockName?.Split('-')?.LastOrDefault()?.Trim();
                    OpAndMaxAssemblySymbol[ze.Key] = maxAsmSymbol;
                }


                foreach (var item in ze)
                {
                    if (item.FileIdentificationType == FileIdentificationType.ZELevelAssembly)
                    {
                        OpProcessedItems.Add(item);
                        continue;
                    }
                    //TODO: Fetch from Database, check for already available number in DB. if it is not in database then check in availabel excel list
                    OperationItemModel refItem = OperationItemData.GetByFileTypeAndItemNumber((PlantModel)this.PlantModel, item.FileIdentificationType, item.ItemNumber);
                    if (refItem == null)
                    {
                        refItem = OpProcessedItems.FirstOrDefault(_ => _.FileIdentificationType == item.FileIdentificationType && _.ItemNumber == item.ItemNumber);
                    }
                    if (refItem != null)
                    {
                        item.NewSheetNumber = refItem.SheetNumber;
                        item.ReferanceSheetNumber = refItem.SheetNumber;
                        item.ReferanceZENumber = refItem.ZEDrawingNumber;
                        item.ReferanceSymbol = refItem.Symbol;
                        OpProcessedItems.Add(item);
                    }
                    else
                    {
                        int maxSheetNo = OpAndMaxSheetNo[ze.Key] + 1;
                        item.NewSheetNumber = maxSheetNo.ToString().PadLeft(3, '0');

                        if (item.FileIdentificationType == FileIdentificationType.Part)
                        {
                            int maxPartSymbol = OpAndMaxPartSymbol[ze.Key] + 1;
                            item.NewSymbol = maxPartSymbol.ToString().PadLeft(3, '0');
                            OpAndMaxPartSymbol[ze.Key] = maxPartSymbol;
                        }

                        if (item.FileIdentificationType == FileIdentificationType.Assembly)
                        {
                            if (!item.FileName.Equals(item.ItemNumber))
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

        private void processDataForHexNo(ref ObservableCollection<OperationItemModel> list)
        {
            var zeRefPartsGroup = list.Where(_ => !string.IsNullOrEmpty(_.ReferanceZENumber) && _.FileIdentificationType == FileIdentificationType.Part && _.ZEDrawingNumber != _.ReferanceZENumber)
                .OrderBy(_ => _.RowIndex)
                .GroupBy(_ => _.ZEDrawingNumber.Trim());
            foreach (var ze in zeRefPartsGroup)
            {
                var refParts = ze
                    .OrderBy(_ => _.RowIndex)
                    .GroupBy(_ => _.ZEDrawingNumber);

                foreach (var item in refParts)
                {
                    int hxNo = 0;
                    var refZeGroups = item.OrderBy(_ => _.RowIndex).GroupBy(_ => _.ReferanceZENumber.Trim());
                    foreach (var refZeGroup in refZeGroups)
                    {
                        hxNo++;
                        foreach (var part in refZeGroup)
                        {
                            part.HEXNumber = $"{hxNo}H";
                        }
                    }
                }
            }
        }
        #endregion
    }
}
