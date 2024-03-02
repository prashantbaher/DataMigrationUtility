using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.Models
{
    public class AssemblyLineModel
    {
        public int LineId { get; set; }
        public int PlantId { get; set; }
        public string LineName { get; set; }
        public string LineDescription { get; set; }        
    }
}
