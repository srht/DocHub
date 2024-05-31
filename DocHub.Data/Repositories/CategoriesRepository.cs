using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Repositories
{
    public class CategoriesRepository : EFCoreRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(DocHubDbContext docHubDbContext) : base(docHubDbContext)
        {
        }

        public List<Category> GetWithSubCategories()
        {
            return Dbset.Include("SubCategories").ToList();
        }

        public override Category GetObjectByIntId(int id)
        {
            var found = Dbset.Include("Parent").FirstOrDefault(i=>i.Id==id);
            return found;
        }

        public void Attach(Category category)
        {
            Dbset.Attach(category);
        }

        public override Task SoftDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task SoftDeleteByIntIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateAsync(Category obj)
        {
            Dbset.Update(obj);
            await CommitAsync();
        }
    }
}
