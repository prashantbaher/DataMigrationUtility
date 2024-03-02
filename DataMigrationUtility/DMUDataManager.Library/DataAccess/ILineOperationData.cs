using DMUDataManager.Library.Models;
using System.Collections.Generic;

namespace DMUDataManager.Library.DataAccess
{
    public interface ILineOperationData
    {
        List<LineOperationModel> GetLineOperations(PlantModel plant, AssemblyLineModel line);
        void SaveData<LineOperationModel>(PlantModel plant, LineOperationModel model);
    }
}