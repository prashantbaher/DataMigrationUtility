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
    public class LineOperationMasterListViewModel : Screen
    {
        private readonly IEventAggregator events;
        private readonly ILineOperationData _lineOperationData;
        public ICompanyModel Company { get; set; }
        public IPlantModel Plant { get; set; }
        public AssemblyLineModel Line { get; set; }
        public string SelectedCompnay { get => Company?.CompanyName; }
        public string SelectedPlant { get => Plant?.PlantName; }
        public string SelectedLine { get => Line?.LineName; }

        public LineOperationMasterListViewModel(IEventAggregator events, ILineOperationData OpData)
        {
            this.events = events;
            this._lineOperationData = OpData;
            EditCommand = new RelayCommand(Edit, CanEdit);
            SelectCommand = new RelayCommand(Select, CanSelect);
        }

        private List<LineOperationModel> _SearchedLineList;
        private List<LineOperationModel> _lineList;
        public List<LineOperationModel> LineOperations
        {
            get => _SearchedLineList;
            set
            {
                _SearchedLineList = value;
                NotifyOfPropertyChange(() => LineOperations);
            }
        }

        public void LoadLineOperations()
        {
            _lineList = _lineOperationData.GetLineOperations((PlantModel)Plant, Line);
            _SearchedLineList = _lineList?.OrderBy(_ => _.OperationName)?.ToList();
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
            LineOperations = _lineList?
                .Where(_ => _.OperationName.ToLower().Contains(SearchText?.ToLower()))?
                .OrderBy(_ => _.OperationName)?
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
            Console.WriteLine("Back from Operation master!");
            events.PublishOnUIThreadAsync(new LineEventModel(Enums.FormActions.Close, Company, Plant, new AssemblyLineModel()));
        }


        private bool _canAddNewLineOperation = true;
        public bool CanAddNewLineOperation
        {
            get => _canAddNewLineOperation;
            set
            {
                _canAddNewLineOperation = value;
                NotifyOfPropertyChange(() => CanAddNewLineOperation);
            }
        }
        public void AddNewLineOperation()
        {
            Console.WriteLine("Add new Assembly Line!");
            events.PublishOnUIThreadAsync(new LineOperationEventModel(Enums.FormActions.Create, this.Company, Plant, Line, new LineOperationModel()));
        }

        private bool _canEdit = true;
        public bool CanEdit(object parameter)
        {
            return _canEdit;
        }
        private void Edit(object parameter)
        {
            Console.WriteLine("Add new Assembly line Operation!");
            events.PublishOnUIThreadAsync(new LineOperationEventModel(Enums.FormActions.Edit, this.Company, this.Plant, Line, (LineOperationModel)parameter));
        }

        private bool _canSelect = true;
        public bool CanSelect(object parameter)
        {
            return _canSelect;
        }
        private void Select(object parameter)
        {
            Console.WriteLine("Select Line Operation!");
            events.PublishOnUIThreadAsync(new LineOperationEventModel(Enums.FormActions.Select, Company, this.Plant, Line, (LineOperationModel)parameter));
        }
        public RelayCommand SelectCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        #endregion
    }
}
