using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class ServerType
    {
        public decimal ServerTypeId { get; set; }
        public string Name { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public string? DeletedBy { get; set; }


    }
}
