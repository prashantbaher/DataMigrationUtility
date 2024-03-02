using Dapper;
using DMUDataManager.Library.Internal.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.DataAccess
{
    public class PlantData : IPlantData
    {
        public List<PlantModel> GetPlants(CompanyModel company)
        {
            SqlDataAccess sqlData = new SqlDataAccess();
            return sqlData.LoadData<PlantModel, dynamic>("[dbo].[spPlantMaster_GetAll]", new { CompanyId = company.CompanyID }, "DatabaseConnection");
        }

        public void SaveData(PlantModel model)
        {
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                CreatePlantDatabase(model);
                DynamicParameters p = new DynamicParameters();
                p.Add("@PlantId", model.PlantId, System.Data.DbType.Int32, System.Data.ParameterDirection.InputOutput);
                p.Add("@CompanyId", model.CompanyId, System.Data.DbType.Int32);
                p.Add("@PlantName", model.PlantName, System.Data.DbType.String);
                p.Add("@PlantCode", model.PlantCode, System.Data.DbType.String);
                p.Add("@PlantLocation", model.PlantLocation, System.Data.DbType.String);
                p.Add("@DBName", model.DBName, System.Data.DbType.String);
                sqlData.SaveData<dynamic>("[dbo].[spPlantMaster_AddEdit]", p, "DatabaseConnection");
                model.PlantId = p.Get<Int32>("@PlantId");
            }
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection", model.DBName);
                    sqlData.SaveDataInTransaction("[dbo].[spPlantMaster_Insert]", new { PlantId = model.PlantId, PlantName = model.PlantName });
                    sqlData.CommitTransaction();
                }
                catch
                {
                    sqlData.RollBackTransaction();
                }
            }
        }

        private bool CreatePlantDatabase(PlantModel model)
        {
            if (model.PlantId == 0)
            {
                CreateDatabaseRequest request = new CreateDatabaseRequest();
                var newString = request.CreateDatabse("DatabaseConnection", model.DBName);

            }
            return true;
        }

    }
}
