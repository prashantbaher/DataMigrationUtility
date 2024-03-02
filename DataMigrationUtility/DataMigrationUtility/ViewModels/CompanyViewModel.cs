using Caliburn.Micro;
using DataMigrationUtility.EventModels.Company;
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.ViewModels
{
    public class CompanyViewModel : Screen
    {
        private readonly IEventAggregator events;
        private readonly ICompanyData _companyData;

        public CompanyViewModel(IEventAggregator events, ICompanyData companyData, ICompanyModel model)
        {
            this.events = events;
            this._companyData = companyData;

            if (model == null)
            {
                model = IoC.Get<CompanyModel>();
            }
        }

        private ICompanyModel _Model;

        public ICompanyModel Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                NotifyOfPropertyChange(() => _Model);
            }
        }

        private int _Id;
        public int Id
        {
            get { return Model.CompanyID; }
            set
            {
                _Id = value;
                Model.CompanyID = value;
                NotifyOfPropertyChange(() => _Id);
            }
        }

        private string companyName;
        public string CompanyName
        {
            get { return Model.CompanyName; }
            set
            {
                companyName = value;
                Model.CompanyName = value;
                NotifyOfPropertyChange(() => companyName);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        private string companyCode;
        public string CompanyCode
        {
            get { return Model.CompanyCode; }
            set
            {
                companyCode = value;
                Model.CompanyCode = value;
                NotifyOfPropertyChange(() => companyCode);
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        private string _CompanyDescription;
        public string CompanyDescription
        {
            get { return Model.CompanyDescription; }
            set
            {
                _CompanyDescription = value;
                Model.CompanyDescription = value;
                NotifyOfPropertyChange(() => _CompanyDescription);
            }
        }


        #region Actions
        public bool CanSave
        {
            get => !string.IsNullOrEmpty(Model.CompanyName) && !string.IsNullOrEmpty(Model.CompanyCode);            
        }

        public void Save()
        {
            Console.WriteLine("Save!");
            _companyData.SaveData(Model);
            events.PublishOnUIThreadAsync(new CompanyListEventModel(Enums.FormActions.Save, new CompanyModel()));
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
            events.PublishOnUIThreadAsync(new CompanyListEventModel(Enums.FormActions.Close, new CompanyModel()));
        }
        #endregion
    }
}
