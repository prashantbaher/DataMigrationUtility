using DMUDataManager.Library.Models;
using System.Collections.Generic;

namespace DMUDataManager.Library.DataAccess
{
    public interface ICompanyData
    {
        List<CompanyModel> GetCompanies();
        void SaveData<CompanyModel>(CompanyModel model);
    }
}