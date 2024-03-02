using DataMigrationUtility.Enums;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationUtility.EventModels.Plant
{
    public class PlantEventModel
    {
        public FormActions Actions { get; }
        public ICompanyModel CompnayModel { get; }
        public IPlantModel PlantModel { get; }

        public PlantEventModel(FormActions actions, ICompanyModel companyModel, IPlantModel plantModel)
        {
            Actions = actions;
            CompnayModel = companyModel;
            PlantModel = plantModel;
        }
    }
}
