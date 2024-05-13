using DocHub.Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Abstracts
{
    public interface IFileStoreService
    {
         Task<string> SaveFileAsync(FileStoreObject fileStoreObject);
        bool CheckIfExcelFile(IFormFile file);
        bool CheckIfImageFile(string extension);
    }
}
