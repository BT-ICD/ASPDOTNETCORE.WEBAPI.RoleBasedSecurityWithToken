using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class ServerTypeDTOAddNewServerType
    {
        public decimal ServerTypeId { get; set; }
        public string Name { get; set; }
        public string? CreatedBy { get; set; }
    }
}
