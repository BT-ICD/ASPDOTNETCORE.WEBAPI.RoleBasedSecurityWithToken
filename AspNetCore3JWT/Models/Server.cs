using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class Server
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? ServerId { get; set; }
        public string ServerName { get; set; }
        public decimal ServerTypeId { get; set; }
        public string? InternalIP { get; set; }
        public string? ExternalIP { get; set; }
        public string? URLToAccess { get; set; }
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastUpdatedBy { get; set; }


    }
}
