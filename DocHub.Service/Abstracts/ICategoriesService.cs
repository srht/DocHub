﻿using DocHub.Common.DTO;
using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Abstracts
{
    public interface ICategoriesService
    {
        List<CategoryDto> GetCategories();
        CategoryDto GetCategoryDto(int id);
        void AddCategory(CategoryDto categoryDto);
        void UpdateCategory(CategoryDto categoryDto);
        void DeleteCategory(int id);
        
    }
}
