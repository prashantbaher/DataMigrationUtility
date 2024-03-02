using Caliburn.Micro;
using DataMigrationUtility.EventModels.Company;
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
    public class PlantViewModel : Screen
    {
        public IEventAggregator Events { get; }
        public IPlantData PlantData { get; }
        public ICompanyModel CompanyModel { get; set; }
        public IPlantModel PlantModel { get; set; }
        public PlantViewModel(IEventAggregator events, IPlantData plantData)
        {
            Events = events;
            PlantData = plantData;
        }

        public string CompanyName
        {
            get { return CompanyModel.CompanyName; }
        }
        public int PlantId
        {
            get { return PlantModel.PlantId; }
            set
            {
                PlantModel.PlantId = value;
                NotifyOfPropertyChange(() => PlantId);
            }
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
        public string PlantCode
        {
            get { return PlantModel.PlantCode; }
            set
            {
                PlantModel.PlantCode = value;
                NotifyOfPropertyChange(() => PlantCode);
                NotifyOfPropertyChange(() => CanSave);
            }
        }
        public string PlantLocation
        {
            get { return PlantModel.PlantLocation; }
            set
            {
                PlantModel.PlantLocation = value;
                NotifyOfPropertyChange(() => PlantLocation);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        #region Actions
        private bool _canSave = false;
        public bool CanSave
        {
            get
            {
                _canSave = !string.IsNullOrEmpty(PlantName) && !string.IsNullOrEmpty(PlantCode) && !string.IsNullOrEmpty(PlantLocation);
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
            PlantModel.CompanyId = CompanyModel.CompanyID;
            PlantModel.DBName = $"{CompanyModel.CompanyCode}_{PlantModel.PlantCode}_DB";
            PlantData.SaveData((PlantModel)PlantModel);
            Events.PublishOnUIThreadAsync(new PlantEventModel(Enums.FormActions.Save, CompanyModel, new PlantModel()));
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
