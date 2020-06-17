using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Reference URL to learn : 
/// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1
/// https://stackoverflow.com/questions/39037049/how-to-upload-a-file-and-json-data-in-postman
/// https://www.c-sharpcorner.com/article/reading-values-from-appsettings-json-in-asp-net-core/
/// </summary>
namespace AspNetCore3JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileUploadDemoController : ControllerBase
    {
        private readonly IConfiguration config;
        public FileUploadDemoController(IConfiguration config)
        {
            this.config = config;
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpload(IFormFile file)
        {
            long size = file.Length;
            //To get free file name 
            //var filePath = Path.GetTempFileName();

            //To get location (path) where application deployed - AppContext.BaseDirectory
            //Reference URL: https://stackoverflow.com/questions/16557122/microsoft-web-api-how-do-you-do-a-server-mappath
            var documentFolderName = config.GetSection("MyAppSettings").GetSection("UploadDocumentFolder").Value;
            var fileName = file.FileName;
            var filePathDocument = AppContext.BaseDirectory + documentFolderName + "\\" + fileName;

            using (var stream = System.IO.File.Create(filePathDocument))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new { result = "sucess", contentSize = size });
        }
        public IActionResult GetHello()
        {
            //To access application folder path
            string baseDirectory = AppContext.BaseDirectory;
            var documentFolder = config.GetSection("MyAppSettings").GetSection("UploadDocumentFolder").Value;
            return Ok("Hello World From File");
        }
    }

}