using eTagTech.SDK.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_ANSPRICING.Models
{
    public class Tag
    {

        public Guid id { get; set; }
        public Guid StationId { get; set; }
        [ForeignKey("StationId")]
        public Station station { get; set; }
        public string tagId { get; set; }
        public ESLType type { get; set; }
        public string name { get; set; }
        public string coutry { get; set; }
        public string manufacturer { get; set; }
        public string oldPrice { get; set; }
        public string price { get; set; }
        public string description1 { get; set; }
        public string description2 { get; set; }
        public string description3 { get; set; }
        public string description4 { get; set; }
        public string description5 { get; set; }
        public string description6 { get; set; }
        public string imgSource { get; set; }
        public string QrCode { get; set; }
    }
}
