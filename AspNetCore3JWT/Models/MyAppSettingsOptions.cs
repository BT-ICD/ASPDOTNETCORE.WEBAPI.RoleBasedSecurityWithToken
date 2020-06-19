using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3JWT.Models
{
    public class MyAppSettingsOptions
    {
        public const string MyAppSettings = "MyAppSettings";
        public string UploadDocumentFolder { get; set; }
        public bool IsLoggingAllowed { get; set; }
        
    }
}
