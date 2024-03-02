using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMUDataManager.Library.Models
{
    public class LineOperationModel
    {
        public int OperationId { get; set; }
        public int AssemblyLineId { get; set; }
        public string OperationName { get; set; }
        public string OperationCode { get; set; }
        public string InputLocation { get; set; }
        public string OutputLocation { get; set; }
    }
}
