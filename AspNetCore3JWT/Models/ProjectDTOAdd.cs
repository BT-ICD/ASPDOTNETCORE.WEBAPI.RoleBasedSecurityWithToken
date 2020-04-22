using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class ProjectDTOAdd
    {
        public string Name { get; set; }
        public string? About { get; set; }
        public string? Notes { get; set; }
        public string? SourceCodeLocation { get; set; }
    }
}
