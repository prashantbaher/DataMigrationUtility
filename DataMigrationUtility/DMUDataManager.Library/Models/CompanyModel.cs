using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.Models
{
    public class CompanyModel : ICompanyModel
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyDescription { get; set; }
    }
}
