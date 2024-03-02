using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataMigrationUtility.EventModels;
using DataMigrationUtility.EventModels.Company;
using DataMigrationUtility.EventModels.Line;
using DataMigrationUtility.EventModels.LineOperation;
using DataMigrationUtility.EventModels.Plant;
using DataMigrationUtility.Views;
using DMUDataManager.Library.DataAccess;

namespace DataMigrationUtility.ViewModels
{
    public class MainWindowViewModel : Conductor<Object>,
        IHandle<CompanyListEventModel>,
        IHandle<PlantEventModel>,
        IHandle<LineEventModel>,
        IHandle<LineOperationEventModel>,
        IHandle<ProcessOperationEventModel>
    {
        private readonly IEventAggregator _events;
        private readonly IDataSettings _masterData;

        public MainWindowViewModel(IEventAggregator events, IDataSettings masterData)
        {
            this._events = events;
            this._masterData = masterData;
            _ConnectedServer = _masterData.GetServerName();
            _events.SubscribeOnUIThread(this);
            ActivateItemAsync(IoC.Get<CompanyListViewModel>());
        }

        private string _ConnectedServer = "192.168.0.1/SQLEXPRESS";
        public string ConnectedServer
        {
            get => _ConnectedServer;
            set
            {
                _ConnectedServer = value;
                NotifyOfPropertyChange(() => _ConnectedServer);
            }
        }

        private DateTime _CurrentDate = DateTime.Now;


        public DateTime CurrentDate
        {
            get => _CurrentDate;
        }
        #region Events Region
        public Task HandleAsync(CompanyListEventModel message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message);
            switch (message.EventAction)
            {
                case Enums.FormActions.Create:
                case Enums.FormActions.Edit:
                    var companyview = IoC.Get<CompanyViewModel>();
                    companyview.Model = message.Model;
                    ActivateItemAsync(companyview);
                    break;
                case Enums.FormActions.Close:
                case Enums.FormActions.Save:
                    ActivateItemAsync(IoC.Get<CompanyListViewModel>());
                    break;
                case Enums.FormActions.Delete:
                    break;
                case Enums.FormActions.Select:
                    var plantListView = IoC.Get<PlantListViewModel>();
                    plantListView.Company = message.Model;
                    plantListView.LoadPlants();
                    ActivateItemAsync(plantListView);
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        public Task HandleAsync(PlantEventModel message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message);
            switch (message.Actions)
            {
                case Enums.FormActions.Create:
                case Enums.FormActions.Edit:
                    var view = IoC.Get<PlantViewModel>();
                    view.PlantModel = message.PlantModel;
                    view.CompanyModel = message.CompnayModel;
                    ActivateItemAsync(view);
                    break;
                case Enums.FormActions.Close:
                case Enums.FormActions.Save:
                    var PlantListView = IoC.Get<PlantListViewModel>();
                    PlantListView.Company = message.CompnayModel;
                    PlantListView.LoadPlants();
                    ActivateItemAsync(PlantListView);
                    break;
                case Enums.FormActions.Delete:
                    break;
                case Enums.FormActions.Select:
                    var listView = IoC.Get<AssemblyLineMasterListViewModel>();
                    listView.Company = message.CompnayModel;
                    listView.Plant = message.PlantModel;
                    listView.LoadLines();
                    ActivateItemAsync(listView);
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        public Task HandleAsync(LineEventModel message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message);
            switch (message.Actions)
            {
                case Enums.FormActions.Create:
                case Enums.FormActions.Edit:
                    var view = IoC.Get<AssemblyLineMasterViewModel>();
                    view.PlantModel = message.PlantModel;
                    view.CompanyModel = message.CompanyModel;
                    view.LineModel = message.LineModel;
                    ActivateItemAsync(view);
                    break;
                case Enums.FormActions.Close:
                case Enums.FormActions.Save:
                    var listView = IoC.Get<AssemblyLineMasterListViewModel>();
                    listView.Company = message.CompanyModel;
                    listView.Plant = message.PlantModel;
                    listView.LoadLines();
                    ActivateItemAsync(listView);
                    break;
                case Enums.FormActions.Delete:
                    break;
                case Enums.FormActions.Select:
                    var OplistView = IoC.Get<LineOperationMasterListViewModel>();
                    OplistView.Company = message.CompanyModel;
                    OplistView.Plant = message.PlantModel;
                    OplistView.Line = message.LineModel;
                    OplistView.LoadLineOperations();
                    ActivateItemAsync(OplistView);
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        public Task HandleAsync(LineOperationEventModel message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message);
            switch (message.Actions)
            {
                case Enums.FormActions.Create:
                case Enums.FormActions.Edit:
                    var view = IoC.Get<LineOperationMasterViewModel>();
                    view.PlantModel = message.PlantModel;
                    view.CompanyModel = message.CompanyModel;
                    view.LineModel = message.LineModel;
                    view.OperationModel = message.OperationModel;
                    ActivateItemAsync(view);
                    break;
                case Enums.FormActions.Close:
                case Enums.FormActions.Save:
                    var OplistView = IoC.Get<LineOperationMasterListViewModel>();
                    OplistView.Company = message.CompanyModel;
                    OplistView.Plant = message.PlantModel;
                    OplistView.Line = message.LineModel;
                    OplistView.LoadLineOperations();
                    ActivateItemAsync(OplistView);
                    break;
                case Enums.FormActions.Delete:
                    break;
                case Enums.FormActions.Select:
                    var OpItemView = IoC.Get<ProcessOperationItemViewModel>();
                    OpItemView.CompanyModel = message.CompanyModel;
                    OpItemView.PlantModel = message.PlantModel;
                    OpItemView.LineModel = message.LineModel;
                    OpItemView.OperationModel = message.OperationModel;
                    ActivateItemAsync(OpItemView);
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }

        public Task HandleAsync(ProcessOperationEventModel message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message);
            switch (message.Actions)
            {
                case Enums.FormActions.Create:
                case Enums.FormActions.Edit:
                case Enums.FormActions.Save:
                case Enums.FormActions.Delete:
                case Enums.FormActions.Select:
                    break;
                case Enums.FormActions.Close:
                    var OplistView = IoC.Get<LineOperationMasterListViewModel>();
                    OplistView.Company = message.CompanyModel;
                    OplistView.Plant = message.PlantModel;
                    OplistView.Line = message.LineModel;
                    OplistView.LoadLineOperations();
                    ActivateItemAsync(OplistView);
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }
        #endregion
    }
}
