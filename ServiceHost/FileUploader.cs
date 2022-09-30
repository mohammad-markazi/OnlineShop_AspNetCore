using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceHost
{
    public class FileUploader:IFileUploader
    {
        private readonly IWebHostEnvironment
             _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file,params string[] paths)
        {
            if(file == null)
                return String.Empty;

            var pathInput = String.Join('\\',paths);
            
            var directoryPath = Path.Join(_webHostEnvironment.WebRootPath, "Files",pathInput);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = DateTime.Now.ToFileTime() + "-" + file.FileName;

            var filePath = Path.Combine(directoryPath, fileName);

            using var output = File.Create(filePath);
            file.CopyTo(output);
            return $"{String.Join('/',paths)}/{fileName}";
        }

        public void RemoveFile(string pathFile)
        {
            if (!string.IsNullOrWhiteSpace(pathFile))
            {
                pathFile = pathFile.Replace("/", "\\");
                var pathFilePhysics = Path.Combine(_webHostEnvironment.WebRootPath, "Files", pathFile);
                if (File.Exists(pathFilePhysics))
                    File.Delete(pathFilePhysics);
            }
      
        }
    }
}
