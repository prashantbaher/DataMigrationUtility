using DataMigrationUtility.Enums;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.EventModels.Plant
{
    public class ProcessOperationEventModel
    {
        public FormActions Actions { get; }
        public ICompanyModel CompanyModel { get; }
        public IPlantModel PlantModel { get; }
        public AssemblyLineModel LineModel { get; }
        public LineOperationModel OperationModel { get; }

        public ProcessOperationEventModel(FormActions actions, 
            ICompanyModel companyModel, 
            IPlantModel plantModel,
            AssemblyLineModel lineModel, 
            LineOperationModel operationModel)
        {
            Actions = actions;
            CompanyModel = companyModel;
            PlantModel = plantModel;
            LineModel = lineModel;
            OperationModel = operationModel;
        }
    }
}
