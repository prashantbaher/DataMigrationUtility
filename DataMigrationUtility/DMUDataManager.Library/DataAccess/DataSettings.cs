using DMUDataManager.Library.Internal.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.DataAccess
{
    public class DataSettings : IDataSettings
    {
        public string GetServerName()
        {
            using (SqlDataAccess sqlAccess = new SqlDataAccess())
                return sqlAccess.GetServerName("DatabaseConnection");
        }
    }
}
