﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class ServerDTOAdd
    {
        //Not required as ServerID  is Identity Field
        //public decimal ServerID { get; set; }
        public string ServerName { get; set; }
        public decimal ServerTypeId { get; set; }
        public string? InternalIP { get; set; }
        public string? ExternalIP { get; set; }
        public string? URLToAccess { get; set; }
        public string? Notes { get; set; }
       
        
    }
}
