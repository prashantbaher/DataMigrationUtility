using DMUDataManager.Library.Models;
using System.Collections.Generic;

namespace DMUDataManager.Library.DataAccess
{
    public interface IAssemblyLineData
    {
        List<AssemblyLineModel> GetAssemblyLines(PlantModel plant);
        void SaveData<AssemblyLineModel>(PlantModel plant, AssemblyLineModel model);
    }
}