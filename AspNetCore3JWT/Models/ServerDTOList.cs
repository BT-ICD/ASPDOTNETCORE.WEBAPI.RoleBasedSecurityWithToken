using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class ServerDTOList
    {
        public decimal? ServerID { get; set; }
        public string ServerName { get; set; }
        public decimal ServerTypeId { get; set; }
        public string ServerTypeName { get; set; }
        public string? Notes { get; set; }
    }
}
