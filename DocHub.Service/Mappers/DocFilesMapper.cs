using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DocHub.Common.DTO;
using DocHub.Core.Entities;
using DocHub.Data.Repositories;

namespace DocHub.Service.Mappers
{
    public class DocFilesMapper:Profile
    {
        public DocFilesMapper() {
            CreateMap<DDocFilePath, DDocFilePathDto>().ReverseMap();
        }
    }
}
