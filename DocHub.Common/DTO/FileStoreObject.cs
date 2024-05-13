using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.DTO
{
    public class FileStoreObject
    {
        public IFormFile FileObject { get; set; }
    }
}
