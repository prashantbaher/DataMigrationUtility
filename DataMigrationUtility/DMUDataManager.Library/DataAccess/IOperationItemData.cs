using DMUDataManager.Library.Enums;
using DMUDataManager.Library.Models;
using System.Collections.Generic;

namespace DMUDataManager.Library.DataAccess
{
    public interface IOperationItemData
    {
        OperationItemModel GetByFileTypeAndItemNumber(PlantModel plant, FileIdentificationType fileIdentificationType, string itemNumber);
        List<LineOperationModel> GetLineOperations(PlantModel plant, AssemblyLineModel line);
        void SaveData<OperationItemModel>(PlantModel plant, OperationItemModel model);
    }
}