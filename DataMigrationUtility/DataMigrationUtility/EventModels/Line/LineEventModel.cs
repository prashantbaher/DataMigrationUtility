using DataMigrationUtility.Enums;
using DMUDataManager.Library.Models;

namespace DataMigrationUtility.EventModels.Line
{
    public class LineEventModel
    {
        public FormActions Actions { get; }
        public ICompanyModel CompanyModel { get; }
        public IPlantModel PlantModel { get; }
        public AssemblyLineModel LineModel { get; }

        public LineEventModel(FormActions actions, ICompanyModel companyModel, IPlantModel plantModel, AssemblyLineModel lineModel)
        {
            Actions = actions;
            CompanyModel = companyModel;
            PlantModel = plantModel;
            LineModel = lineModel;
        }
    }
}
