using DocHub.Common.DTO;
using DocHub.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service
{
    public class FileStoreService : IFileStoreService
    {
        public FileStoreService(string webRootPath)
        {
            WebRootPath = webRootPath;
        }

        public string WebRootPath { get; set; }
        public string TempDir { get; } = "wwwroot\\Upload\\temp";
        public string TempPath { get; } = "/Upload/temp/";

        public async Task<string> SaveFileAsync(FileStoreObject fileStoreObject)
        {
            IFormFile file = fileStoreObject.FileObject;
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];


            string fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

            var pathBuilt = Path.Combine(WebRootPath, TempDir);

            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }

            string path = Path.Combine(WebRootPath, TempDir, fileName);
            bool isImageFile = CheckIfImageFile(extension);
            /*
            if (isImageFile)
            {
                var pathImageBuilt = Path.Combine(WebRootPath, TempDir + "\\images");

                if (!Directory.Exists(pathImageBuilt))
                {
                    Directory.CreateDirectory(pathImageBuilt);
                }

                string pathImage = Path.Combine(WebRootPath, TempDir + "\\images", fileName);

                /*
                using (Image image = Image.Load(file.OpenReadStream()))
                {
                    image.Mutate(x => x
                        .Resize(255, 365));

                    image.Save(pathImage); // Automatic encoder selected based on extension.
                }
                


                return TempPath + "images/" + fileName;
            }
            else
        */
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return TempPath + fileName;
            }
        }

        public bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".xlsx" || extension == ".xls"); // Change the extension based on your need
        }

        public bool CheckIfImageFile(string extension)
        {
            return (extension == ".png" || extension == ".jpg"); // Change the extension based on your need
        }
    }
}
