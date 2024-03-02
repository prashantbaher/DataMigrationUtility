using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataMigrationUtility.Command;
using DataMigrationUtility.EventModels;
using DataMigrationUtility.EventModels.Company;
using DataMigrationUtility.EventModels.Plant;
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Models;

namespace DataMigrationUtility.ViewModels
{
    public class PlantListViewModel : Screen
    {
        private readonly IEventAggregator events;
        private readonly IPlantData _plantData;
        public ICompanyModel Company { get; set; }
        public PlantListViewModel(IEventAggregator events, IPlantData plantData)
        {
            this.events = events;
            this._plantData = plantData;
            EditCommand = new RelayCommand(Edit, CanEdit);
            SelectCommand = new RelayCommand(Select, CanSelect);

        }
        public string SelectedCompnay { get => Company?.CompanyName; }

        private List<PlantModel> _SearchedPlantList;
        private List<PlantModel> _plantList;
        public List<PlantModel> Plants
        {
            get => _SearchedPlantList;
            set
            {
                _SearchedPlantList = value;
                NotifyOfPropertyChange(() => Plants);
            }
        }

        public void LoadPlants()
        {
            _plantList = _plantData.GetPlants((CompanyModel)Company);
            _SearchedPlantList = _plantList?.OrderBy(_ => _.PlantName)?.ToList();
        }

        private object _CompanyControl;

        public object CompanyControl
        {
            get => _CompanyControl;
            set
            {
                _CompanyControl = value;
                NotifyOfPropertyChange(() => _CompanyControl);
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
                NotifyOfPropertyChange(() => searchText);
            }
        }
        private void Search()
        {
            Plants = _plantList?
                .Where(_ => _.PlantName.ToLower().Contains(SearchText?.ToLower()))?
                .OrderBy(_ => _.PlantName)?
                .ToList();
        }


        #region Events
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
            Console.WriteLine("Back from Plant master!");
            events.PublishOnUIThreadAsync(new CompanyListEventModel(Enums.FormActions.Close, new CompanyModel()));
        }


        private bool _canAddNewPlant = true;
        public bool CanAddNewPlant
        {
            get => _canAddNewPlant;
            set
            {
                _canAddNewPlant = value;
                NotifyOfPropertyChange(() => _canAddNewPlant);
            }
        }

        public void AddNewPlant()
        {
            Console.WriteLine("Add new Plant!");
            events.PublishOnUIThreadAsync(new PlantEventModel(Enums.FormActions.Create, this.Company, new PlantModel()));
        }


        private bool _canEdit = true;
        public bool CanEdit(object parameter)
        {
            return _canEdit;
        }

        private void Edit(object parameter)
        {
            Console.WriteLine("Add new Compnay!");
            events.PublishOnUIThreadAsync(new PlantEventModel(Enums.FormActions.Edit, this.Company, (PlantModel)parameter));
        }
        private bool _canSelect = true;
        public bool CanSelect(object parameter)
        {
            return _canSelect;
        }

        private void Select(object parameter)
        {
            Console.WriteLine("Seelct Plant!");
            events.PublishOnUIThreadAsync(new PlantEventModel(Enums.FormActions.Select, Company, (PlantModel)parameter));
        }

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        #endregion
    }
}
