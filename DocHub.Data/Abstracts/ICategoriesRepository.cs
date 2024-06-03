using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Abstracts
{
    public interface ICategoriesRepository:IGenericRepository<Category>
    {
        List<Category> GetWithSubCategories();
        void Attach(Category category);
        Task InsertAsync(Category category);
        Task<Category> GetObjectByIntIdAsync(int id);
    }
}
