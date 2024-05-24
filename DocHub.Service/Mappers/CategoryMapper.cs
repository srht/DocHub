using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocHub.Common.DTO;
using DocHub.Core.Entities;
using DocHub.Data.Repositories;

namespace DocHub.Service.Mappers
{
    public class CategoryMapper:ICategoryMapper
    {
        public CategoryMapper() {

           
        }

        public CategoryDto GetDto(Category source)
        {
            if(source==null)
                return null;
            var categoryDto = new CategoryDto();
            categoryDto.Id = source.Id;
            categoryDto.Parent = GetDto(source?.Parent);
            categoryDto.Name = source.Name;
            categoryDto.SubCategories = source?.SubCategories?.Select(i => GetDto(i))?.ToList();
            return categoryDto;
        }

        public Category GetDb(CategoryDto source)
        {
            if (source == null)
                return null;
            var categoryDb = new Category();
            var parentCategoryDb =GetDb(source?.Parent);
            categoryDb.Parent = parentCategoryDb;
            categoryDb.Name = source.Name;
            categoryDb.SubCategories=source?.SubCategories.Select(i=>GetDb(i))?.ToList();
            return categoryDb;
        }
    }
}
