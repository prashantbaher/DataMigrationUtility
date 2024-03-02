using Caliburn.Micro;
using DataMigrationUtility.EventModels.Company;
using DataMigrationUtility.EventModels.Line;
using DataMigrationUtility.EventModels.Plant;
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.ViewModels
{
    public class AssemblyLineMasterViewModel : Screen
    {
        public IEventAggregator Events { get; }
        public IAssemblyLineData LineData { get; }
        public ICompanyModel CompanyModel { get; set; }
        public IPlantModel PlantModel { get; set; }
        public AssemblyLineModel LineModel { get; set; }
        public AssemblyLineMasterViewModel(IEventAggregator events, IAssemblyLineData lineData, AssemblyLineModel lineModel)
        {
            Events = events;
            LineData = lineData;
            LineModel = lineModel;
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
        public string LineDescription
        {
            get { return LineModel.LineDescription; }
            set
            {
                LineModel.LineDescription = value;
                NotifyOfPropertyChange(() => LineDescription);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        #region Actions
        private bool _canSave = false;
        public bool CanSave
        {
            get
            {
                _canSave = !string.IsNullOrEmpty(PlantName) && !string.IsNullOrEmpty(LineName) && !string.IsNullOrEmpty(LineDescription);
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
            LineModel.PlantId = PlantModel.PlantId;
            LineData.SaveData((PlantModel)PlantModel, LineModel);
            Events.PublishOnUIThreadAsync(new LineEventModel(Enums.FormActions.Save, CompanyModel, PlantModel, new AssemblyLineModel()));
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
            Events.PublishOnUIThreadAsync(new PlantEventModel(Enums.FormActions.Save, CompanyModel, new PlantModel()));
        }
        #endregion
    }
}
