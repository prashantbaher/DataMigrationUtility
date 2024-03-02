using DMUDataManager.Library.Internal.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.DataAccess
{
    public class AssemblyLineData : IAssemblyLineData
    {
        public List<AssemblyLineModel> GetAssemblyLines(PlantModel plant)
        {
            List<AssemblyLineModel> lines = new List<AssemblyLineModel>();
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection", plant.DBName);
                    lines = sqlData.LoadDataInTransaction<AssemblyLineModel, dynamic>("[dbo].[spAssemblyLineMaster_GetAll]", new { PlantId = plant.PlantId });
                    sqlData.CommitTransaction();
                }
                catch
                {
                    sqlData.RollBackTransaction();
                    throw;
                }
            }
            return lines;
        }
        public void SaveData<AssemblyLineModel>(PlantModel plant, AssemblyLineModel model)
        {
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection", plant.DBName);
                    sqlData.SaveDataInTransaction("[dbo].[spPlantMaster_Insert]", new { PlantId = plant.PlantId, PlantName = plant.PlantName });
                    sqlData.SaveDataInTransaction<AssemblyLineModel>("[dbo].[spAssemblyLineMaster_AddEdit]", model);
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
