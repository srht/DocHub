using DocHub.Common.DTO;
using DocHub.Common.ResultModels;
using DocHub.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace DocHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController
    {
        public FileUploaderController(IFileStoreService fileStoreService)
        {
            FileStoreService = fileStoreService;
        }

        public IFileStoreService FileStoreService { get; }

        [HttpPost]
        public async Task<FileUploadResult> Post([FromForm]IFormFile file)
        {
            FileStoreObject fileStoreObject = new(file);
            string url = await FileStoreService.SaveFileAsync(fileStoreObject);
            return new FileUploadResult { UploadedFilePath = url };
        }
    }
}
