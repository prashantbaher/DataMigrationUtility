using DMUDataManager.Library.Enums;
using DMUDataManager.Library.Internal.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.DataAccess
{
    public class OperationItemData : IOperationItemData
    {
        public List<LineOperationModel> GetLineOperations(PlantModel plant, AssemblyLineModel line)
        {
            throw new NotImplementedException();
            //List<LineOperationModel> operations = new List<LineOperationModel>();
            //using (SqlDataAccess sqlData = new SqlDataAccess())
            //{
            //    try
            //    {
            //        sqlData.StartConnection("DatabaseConnection", plant.DBName);
            //        operations = sqlData.LoadDataInTransaction<LineOperationModel, dynamic>("[dbo].[spLineOperationMaster_GetAll]", new { LineId = line.LineId });
            //        sqlData.CommitTransaction();
            //    }
            //    catch
            //    {
            //        sqlData.RollBackTransaction();
            //        throw;
            //    }
            //}
            //return operations;
        }
        public void SaveData<OperationItemModel>(PlantModel plant, OperationItemModel model)
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

        public OperationItemModel GetByFileTypeAndItemNumber(PlantModel plant, FileIdentificationType fileIdentificationType, string itemNumber)
        {
            OperationItemModel model = null;
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection", plant.DBName);
                    var operations = sqlData.LoadDataInTransaction<OperationItemModel, dynamic>("[dbo].[spOperationItem_GetByTypeAndItemNo]",
                        new
                        {
                            FileIdentificationType = fileIdentificationType,
                            ItemNumber = itemNumber
                        });
                    model = operations?.FirstOrDefault();
                    sqlData.CommitTransaction();
                }
                catch
                {
                    sqlData.RollBackTransaction();
                    throw;
                }
            }

            return model;
        }
    }
}
