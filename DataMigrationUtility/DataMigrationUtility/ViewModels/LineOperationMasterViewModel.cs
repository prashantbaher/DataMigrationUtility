using Caliburn.Micro;
using DataMigrationUtility.EventModels.Company;
using DataMigrationUtility.EventModels.Line;
using DataMigrationUtility.EventModels.LineOperation;
using DataMigrationUtility.EventModels.Plant;
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DataMigrationUtility.ViewModels
{
    public class LineOperationMasterViewModel : Screen
    {
        public IEventAggregator Events { get; }
        public ILineOperationData OperationData { get; }
        public ICompanyModel CompanyModel { get; set; }
        public IPlantModel PlantModel { get; set; }
        public AssemblyLineModel LineModel { get; set; }
        public LineOperationModel OperationModel { get; set; }
        public LineOperationMasterViewModel(IEventAggregator events, ILineOperationData operationData,
            AssemblyLineModel lineModel, LineOperationModel operationModel)
        {
            Events = events;
            OperationData = operationData;
            LineModel = lineModel;
            OperationModel = operationModel;
        }

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
                NotifyOfPropertyChange(() => CanSave);
            }
        }
        public int LineId
        {
            get { return LineModel.LineId; }
            set
            {
                LineModel.LineId = value;
                NotifyOfPropertyChange(() => LineId);
                NotifyOfPropertyChange(() => CanSave);
            }
        }
        public string LineName
        {
            get { return LineModel.LineName; }
            set
            {
                LineModel.LineName = value;
                NotifyOfPropertyChange(() => LineName);
                NotifyOfPropertyChange(() => CanSave);
            }
        }
        public string OperationName
        {
            get { return OperationModel.OperationName; }
            set
            {
                OperationModel.OperationName = value;
                NotifyOfPropertyChange(() => OperationName);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public string OperationCode
        {
            get { return OperationModel.OperationCode; }
            set
            {
                OperationModel.OperationCode = value;
                NotifyOfPropertyChange(() => OperationCode);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public string InputLocation
        {
            get { return OperationModel.InputLocation; }
            set
            {
                OperationModel.InputLocation = value;
                NotifyOfPropertyChange(() => InputLocation);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public string OutputLocation
        {
            get { return OperationModel.OutputLocation; }
            set
            {
                OperationModel.OutputLocation = value;
                NotifyOfPropertyChange(() => OutputLocation);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        #region Actions
        private bool _canSave = false;
        public bool CanSave
        {
            get
            {
                _canSave = !string.IsNullOrEmpty(OperationName)
                    && !string.IsNullOrEmpty(InputLocation)
                    && !string.IsNullOrEmpty(OutputLocation)
                    && !string.IsNullOrEmpty(OperationCode);
                return _canSave;
            }
            set
            {
                _canSave = value;
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public void Save()
        {
            Console.WriteLine("Save!");
            OperationModel.AssemblyLineId = LineModel.LineId;
            OperationData.SaveData((PlantModel)PlantModel, OperationModel);
            Events.PublishOnUIThreadAsync(new LineOperationEventModel(Enums.FormActions.Save, CompanyModel, PlantModel, LineModel, new LineOperationModel()));
        }

        private bool _canClose = true;
        public bool CanClose
        {
            get => _canClose;
            set
            {
                _canClose = value;
                NotifyOfPropertyChange(() => _canClose);
            }
        }

        public void Close()
        {
            Console.WriteLine("Close!");
            Events.PublishOnUIThreadAsync(new LineOperationEventModel(Enums.FormActions.Close, CompanyModel, PlantModel, LineModel, new LineOperationModel()));
        }

        private bool _canInputBrowse = true;
        public bool CanInputBrowse
        {
            get => _canInputBrowse;
            set
            {
                _canInputBrowse = value;
                NotifyOfPropertyChange(() => _canInputBrowse);
            }
        }
        public void InputBrowse()
        {
            Console.WriteLine("Select Input Location!");
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InputLocation = dialog.SelectedPath;
            }
        }

        private bool _canOutputBrowse = true;
        public bool CanOutputBrowse
        {
            get => _canOutputBrowse;
            set
            {
                _canOutputBrowse = value;
                NotifyOfPropertyChange(() => _canOutputBrowse);
            }
        }
        public void OutputBrowse()
        {
            Console.WriteLine("Select output Location!");
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OutputLocation = dialog.SelectedPath;
            }
        }
        #endregion
    }
}
