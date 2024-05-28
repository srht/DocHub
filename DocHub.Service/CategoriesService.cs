using AutoMapper;
using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using DocHub.Service.Abstracts;
using DocHub.Service.Mappers;
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
        public CategoriesService(ICategoriesRepository categoriesRepository, IMapper categoryMapper)
        {
            CategoriesRepository = categoriesRepository;
            CategoryMapper = categoryMapper;
        }

        public ILogger Logger { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public IMapper CategoryMapper { get; }

        public void AddCategory(CategoryDto categoryDto)
        {
            var parentCategoryDb = categoryDto.Parent!=null? CategoriesRepository.GetObjectByIntId(categoryDto.Parent.Id):null;
            var categoryDb = CategoryMapper.Map<Category>(categoryDto);
            categoryDb.Parent = parentCategoryDb;

            CategoriesRepository.Insert(categoryDb);
           
        }

        public void DeleteCategory(int id)
        {
            CategoriesRepository.DeleteByIntId(id);
        }

        public CategoryDto GetCategoryDto(int id)
        {
            var categoryDb=CategoriesRepository.GetObjectByIntId(id);

            var categoryDto= CategoryMapper.Map<CategoryDto>(categoryDb);
            return categoryDto;
        }

        public List<CategoryDto> GetCategories()
        {
            var categories = CategoriesRepository.GetWithSubCategories().Where(i=>!i.IsDeleted).Select(categoryDb => CategoryMapper.Map<CategoryDto>(categoryDb)).ToList();

            return categories;
        }


        public void UpdateCategory(CategoryDto categoryDto)
        {
            var categoryDb = CategoriesRepository.GetObjectByIntId(categoryDto.Id);
            if(!string.IsNullOrEmpty(categoryDto.Name))
            categoryDb.Name=categoryDto.Name;
            if (categoryDto.Parent != null) {
                var parentCat= CategoriesRepository.GetObjectByIntId(categoryDto.Parent.Id);
                categoryDb.Parent = parentCat;
                    }
           
            CategoriesRepository.Update(categoryDb);
        }
    }
}
