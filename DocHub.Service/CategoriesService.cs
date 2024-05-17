using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using DocHub.Service.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service
{
    public class CategoriesService : ICategoriesService
    {
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            CategoriesRepository = categoriesRepository;
        }

        public ILogger Logger { get; }
        public ICategoriesRepository CategoriesRepository { get; }

        public void AddCategory(CategoryDto categoryDto)
        {
           var categoryDb = new Category();
            var parentCategoryDb = categoryDto.Parent!=null? CategoriesRepository.GetObjectByIntId(categoryDto.Parent.Id):null;
            categoryDb.Parent = parentCategoryDb;
            categoryDb.Name=categoryDto.Name;

            CategoriesRepository.Insert(categoryDb);
           
        }

        public void DeleteCategory(int id)
        {
            CategoriesRepository.DeleteByIntId(id);
        }

        public CategoryDto GetCategoryDto(int id)
        {
            var categoryDb=CategoriesRepository.GetObjectByIntId(id);
            var parentCategoryDb=CategoriesRepository.GetObjectByIntId(categoryDb.Parent.Id);
            var parentCategoryDto = new CategoryDto();
            parentCategoryDto = new CategoryDto
            {
                Id=parentCategoryDb.Id,
                Name=parentCategoryDb.Name
            };

            var categoryDto = new CategoryDto();
            categoryDto.Id=categoryDb.Id;
            categoryDto.Parent = parentCategoryDto;
            categoryDto.Name = categoryDb.Name;
            categoryDto.SubCategories=categoryDb?.SubCategories?.Select(i=>new CategoryDto { Id=i.Id, Name=i.Name })?.ToList();
            return categoryDto;
        }

        public List<CategoryDto> GetCategories()
        {
            var categories = CategoriesRepository.GetList().Where(i=>!i.IsDeleted).Select(categoryDb => new CategoryDto
            {
                Id= categoryDb.Id,
                Name=categoryDb.Name,
            }).ToList();

            return categories;
        }


        public void UpdateCategory(CategoryDto categoryDto)
        {
            var categoryDb = CategoriesRepository.GetObjectByIntId(categoryDto.Id);
            if(!string.IsNullOrEmpty(categoryDto.Name))
            categoryDb.Name=categoryDto.Name;
           
            CategoriesRepository.Update(categoryDb);
        }
    }
}
