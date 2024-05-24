using DocHub.Common.DTO;
using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Mappers
{
    public interface ICategoryMapper
    {
        CategoryDto GetDto(Category source);
        Category GetDb(CategoryDto source);
    }
}
