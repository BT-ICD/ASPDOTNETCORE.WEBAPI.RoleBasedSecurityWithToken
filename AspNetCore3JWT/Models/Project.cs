using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal? ProjectId { get; set; }
        public string Name { get; set; }
        public string? About { get; set; }
        public string? Notes { get; set; }
        public string? SourceCodeLocation { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
