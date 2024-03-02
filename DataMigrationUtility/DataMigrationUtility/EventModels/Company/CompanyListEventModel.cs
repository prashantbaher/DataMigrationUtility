using DataMigrationUtility.Enums;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.EventModels.Company
{
    public class CompanyListEventModel
    {
        public FormActions EventAction { get; }
        public ICompanyModel Model { get; }
        public CompanyListEventModel(FormActions _eventAction, ICompanyModel _Model)
        {
            EventAction = _eventAction;
            Model = _Model;
        }
    }
}
