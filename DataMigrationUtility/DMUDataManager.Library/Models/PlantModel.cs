using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.Models
{
    public class PlantModel : IPlantModel
    {
        public int PlantId { get; set; }
        public int CompanyId { get; set; }
        public string PlantName { get; set; }
        public string PlantCode { get; set; }
        public string PlantLocation { get; set; }
        public string DBName { get; set; }
    }
}
