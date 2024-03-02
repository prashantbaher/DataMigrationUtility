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
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Models;

namespace DataMigrationUtility.ViewModels
{
    public class CompanyListViewModel : Screen
    {
        private readonly IEventAggregator events;
        private readonly ICompanyData _companyData;
        public CompanyListViewModel(IEventAggregator events, ICompanyData companyData)
        {
            this.events = events;
            this._companyData = companyData;
            loadCompanies();
            EditCommand = new RelayCommand(Edit, CanEdit);
            SelectCommand = new RelayCommand(Select, CanSelect);

        }
        private List<CompanyModel> _CompanyList;
        private List<CompanyModel> _SearchedCompanyList;
        public List<CompanyModel> Companies
        {
            get => _SearchedCompanyList;
            set
            {
                _SearchedCompanyList = value;
                NotifyOfPropertyChange(() => Companies);
            }
        }

        private void loadCompanies()
        {
            _CompanyList = _companyData.GetCompanies();
            _SearchedCompanyList = _CompanyList?.OrderBy(_ => _.CompanyName)?.ToList();
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                SearchCompany();
                NotifyOfPropertyChange(() => searchText);
            }
        }
        private void SearchCompany()
        {
            Companies = _CompanyList?.Where(_ => _.CompanyName.ToLower().Contains(SearchText?.ToLower()))?.OrderBy(_ => _.CompanyName)?.ToList();
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

        #region Add New Company
        private bool _canAddNewCompany = true;
        public bool CanAddNewCompany
        {
            get => _canAddNewCompany;
            set
            {
                _canAddNewCompany = value;
                NotifyOfPropertyChange(() => _canAddNewCompany);
            }
        }

        public void AddNewCompany()
        {
            Console.WriteLine("Add new Compnay!");
            events.PublishOnUIThreadAsync(new CompanyListEventModel(Enums.FormActions.Create, new CompanyModel()));
        }


        private bool _canEdit = true;
        public bool CanEdit(object parameter)
        {
            return _canEdit;
        }

        private void Edit(object parameter)
        {
            Console.WriteLine("Add new Compnay!");
            events.PublishOnUIThreadAsync(new CompanyListEventModel(Enums.FormActions.Edit, (CompanyModel)parameter));
        }

        private bool _canSelect = true;
        public bool CanSelect(object parameter)
        {
            return _canSelect;
        }

        private void Select(object parameter)
        {
            Console.WriteLine("Add new Compnay!");
            events.PublishOnUIThreadAsync(new CompanyListEventModel(Enums.FormActions.Select, (CompanyModel)parameter));
        }

        public RelayCommand SelectCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        #endregion
    }
}
