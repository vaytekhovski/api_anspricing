using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ANSPRICING.Models
{
    public class Station
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string PORT { get; set; }
        public string stationID { get; set; }
        public string shopCode { get; set; }
    }
}
