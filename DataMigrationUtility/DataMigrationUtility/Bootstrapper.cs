using Caliburn.Micro;
using DataMigrationUtility.ViewModels;
using DMUDataManager.Library.DataAccess;
using DMUDataManager.Library.Models;
using ReadExcel.Library;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataMigrationUtility
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            //var excelProcessedData = ExcelUtil.ReadExcel($@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Input\AE.0010.xlsx");
            Initialize();
        }
        protected override void Configure()
        {
            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IDataSettings, DataSettings>()
                .PerRequest<ICompanyData, CompanyData>()
                .PerRequest<IPlantData, PlantData>()
                .PerRequest<IAssemblyLineData, AssemblyLineData>()
                .PerRequest<ILineOperationData, LineOperationData>()
                .PerRequest<IOperationItemData, OperationItemData>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<MainWindowViewModel>();
            // base.OnStartup(sender, e);
        }
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
