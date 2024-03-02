using DMUDataManager.Library.Internal.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.DataAccess
{
    public class LineOperationData : ILineOperationData
    {
        public List<LineOperationModel> GetLineOperations(PlantModel plant, AssemblyLineModel line)
        {
            List<LineOperationModel> operations = new List<LineOperationModel>();
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection", plant.DBName);
                    operations = sqlData.LoadDataInTransaction<LineOperationModel, dynamic>("[dbo].[spLineOperationMaster_GetAll]", new { LineId = line.LineId });
                    sqlData.CommitTransaction();
                }
                catch
                {
                    sqlData.RollBackTransaction();
                    throw;
                }
            }
            return operations;
        }
        public void SaveData<LineOperationModel>(PlantModel plant, LineOperationModel model)
        {
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection", plant.DBName);
                    sqlData.SaveDataInTransaction("[dbo].[spLineOperationMaster_AddEdit]", model);
                    sqlData.CommitTransaction();
                }
                catch
                {
                    sqlData.RollBackTransaction();
                    throw;
                }
            }
        }
    }
}
