using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore3JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

/// <summary>
/// Reference URL to learn : 
/// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1
/// https://stackoverflow.com/questions/39037049/how-to-upload-a-file-and-json-data-in-postman
/// https://www.c-sharpcorner.com/article/reading-values-from-appsettings-json-in-asp-net-core/
/// https://stackoverflow.com/questions/42460198/return-file-in-asp-net-core-web-api
/// https://codeburst.io/options-pattern-in-net-core-a50285aeb18d
/// 
/// https://www.c-sharpcorner.com/article/upload-download-files-in-asp-net-core-2-0/
/// Local Deployed URL: http://localhost:7458/API/FileUploadDemo/OnPostUploadFiles
/// </summary>
namespace AspNetCore3JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileUploadDemoController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly MyAppSettingsOptions appsettingoptions;
        public FileUploadDemoController(IConfiguration config, IOptions<MyAppSettingsOptions> options)
        {
            this.config = config;
            this.appsettingoptions = options.Value;


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
        /// <summary>
        /// To post multiple files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> OnPostUploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            foreach (var formfile in files)
            {
                if (formfile.Length > 0)
                {
                    var documentFolderName = config.GetSection("MyAppSettings").GetSection("UploadDocumentFolder").Value;
                    var fileName = formfile.FileName;
                    var filePathDocument = AppContext.BaseDirectory + documentFolderName + "\\" + fileName;
                    using (var stream = System.IO.File.Create(filePathDocument))
                    {
                        await formfile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size });
        }
        [HttpGet]
        [Route("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return NotFound("File does not exist");
            var documentFolderName = config.GetSection("MyAppSettings").GetSection("UploadDocumentFolder").Value;
            var path = Path.Combine(Directory.GetCurrentDirectory(), documentFolderName, filename);
            if (!System.IO.File.Exists(path))
                return NotFound("File does not exist");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }
        public IActionResult GetHello()
        {
            //To access application folder path
            string baseDirectory = AppContext.BaseDirectory;

            var documentFolder = config.GetSection("MyAppSettings").GetSection("UploadDocumentFolder").Value;
            return Ok("Hello World From File");
        }
        /// <summary>
        /// Method to access App Settings Values using Depedency Injection
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAppSettings()
        {
            var isAllwed = appsettingoptions.IsLoggingAllowed;
            var docPath = appsettingoptions.UploadDocumentFolder;
            return Ok(appsettingoptions);
        }
        /// <summary>
        /// Learning Resources - 
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httprequest?view=aspnetcore-3.1
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformcollection?view=aspnetcore-3.1
        /// https://stackoverflow.com/questions/51892706/asp-net-core-web-api-file-upload-and-form-data-multiple-parameter-passing-to-m
        /// Reading Multipart - Form Data - https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/sending-html-form-data-part-2
        /// </summary>

        //    [HttpPost]
        //    public async Task<IActionResult> Upload([FromForm] SampleModelWithFile obj)
        //    {

        //        long size = obj.file.Length;
        //        var documentFolderName = myAppSettingsOptions.ProjectDocuments;
        //        var fileName = obj.file.FileName;
        //        var uploadPathWithFileName = Path.Combine(AppContext.BaseDirectory, documentFolderName, fileName);
        //        using (var stream = System.IO.File.Create(uploadPathWithFileName))
        //        {
        //            await obj.file.CopyToAsync(stream);
        //        }


        //        /* To access details using HttpContext.Request.Form - this does not requir any argument in Upload Method - user can pass any number of field in Formdata
        //            int projectId1;
        //            string projectName1;
        //            var form = HttpContext.Request.Form; 
        //            var request = HttpContext.Request;
        //            var files = form.Files;
        //            foreach (var f in files)
        //            {
        //                var filename = f.FileName;
        //            }
        //            if (form.ContainsKey("projectId"))
        //            {
        //               projectId1= Convert.ToInt32( form["projectId"] );
        //                projectName1 = form["projectName"];
        //            } 
        //        */
        //        // HttpContext.Request.Body
        //        return Ok();
        //    }

        //}

    }
}