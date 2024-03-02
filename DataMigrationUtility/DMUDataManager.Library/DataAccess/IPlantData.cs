using DMUDataManager.Library.Models;
using System.Collections.Generic;

namespace DMUDataManager.Library.DataAccess
{
    public interface IPlantData
    {
        List<PlantModel> GetPlants(CompanyModel company);
        void SaveData(PlantModel model);
    }
}