using DMUDataManager.Library.Internal.DataAccess;
using DMUDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.DataAccess
{
    public class CompanyData : ICompanyData
    {
        public List<CompanyModel> GetCompanies()
        {
            List<CompanyModel> companies = new List<CompanyModel>();
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection");
                    companies = sqlData.LoadDataInTransaction<CompanyModel, dynamic>("[dbo].[spCompany_GetAll]", new { });
                    sqlData.CommitTransaction();
                }
                catch
                {
                    sqlData.RollBackTransaction();
                    throw;
                }
            }
            return companies;
        }

        public void SaveData<CompanyModel>(CompanyModel model)
        {
            using (SqlDataAccess sqlData = new SqlDataAccess())
            {
                try
                {
                    sqlData.StartConnection("DatabaseConnection");
                    sqlData.SaveDataInTransaction<CompanyModel>("[dbo].[spCompany_AddEdit]", model);
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
