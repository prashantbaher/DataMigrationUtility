using Caliburn.Micro;
using DataMigrationUtility.Command;
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

namespace DataMigrationUtility.ViewModels
{
    public class AssemblyLineMasterListViewModel : Screen
    {
        private readonly IEventAggregator events;
        private readonly IAssemblyLineData _lineData;
        public ICompanyModel Company { get; set; }
        public IPlantModel Plant { get; set; }
        public string SelectedCompnay { get => Company?.CompanyName; }
        public string SelectedPlant { get => Plant?.PlantName; }

        public AssemblyLineMasterListViewModel(IEventAggregator events, IAssemblyLineData lineData)
        {
            this.events = events;
            this._lineData = lineData;
            EditCommand = new RelayCommand(Edit, CanEdit);
            SelectCommand = new RelayCommand(Select, CanSelect);
        }

        private List<AssemblyLineModel> _SearchedLineList;
        private List<AssemblyLineModel> _lineList;
        public List<AssemblyLineModel> AssemblyLines
        {
            get => _SearchedLineList;
            set
            {
                _SearchedLineList = value;
                NotifyOfPropertyChange(() => AssemblyLines);
            }
        }

        public void LoadLines()
        {
            _lineList = _lineData.GetAssemblyLines((PlantModel)Plant);
            _SearchedLineList = _lineList?.OrderBy(_ => _.LineName)?.ToList();
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
            AssemblyLines = _lineList?
                .Where(_ => _.LineName.ToLower().Contains(SearchText?.ToLower()))?
                .OrderBy(_ => _.LineName)?
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
            Console.WriteLine("Back from Line master!");
            events.PublishOnUIThreadAsync(new PlantEventModel(Enums.FormActions.Close, Company, new PlantModel()));
        }


        private bool _canAddNewLine = true;
        public bool CanAddNewLine
        {
            get => _canAddNewLine;
            set
            {
                _canAddNewLine = value;
                NotifyOfPropertyChange(() => _canAddNewLine);
            }
        }
        public void AddNewLine()
        {
            Console.WriteLine("Add new Assembly Line!");
            events.PublishOnUIThreadAsync(new LineEventModel(Enums.FormActions.Create, this.Company, Plant, new AssemblyLineModel()));
        }

        private bool _canEdit = true;
        public bool CanEdit(object parameter)
        {
            return _canEdit;
        }
        private void Edit(object parameter)
        {
            Console.WriteLine("Add new Assembly line!");
            events.PublishOnUIThreadAsync(new LineEventModel(Enums.FormActions.Edit, this.Company, this.Plant, (AssemblyLineModel)parameter));
        }

        private bool _canSelect = true;
        public bool CanSelect(object parameter)
        {
            return _canSelect;
        }
        private void Select(object parameter)
        {
            Console.WriteLine("Add new Compnay!");
            events.PublishOnUIThreadAsync(new LineEventModel(Enums.FormActions.Select, Company, this.Plant, (AssemblyLineModel)parameter));
        }

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        #endregion
    }
}
