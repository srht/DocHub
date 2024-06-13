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
    public class TagsRepository : EFCoreRepository<Tag>, ITagsRepository
    {
        public TagsRepository(DocHubDbContext docHubDbContext) : base(docHubDbContext)
        {
        }

        public async Task<Tag> GetObjectByIntIdAsync(int id)
        {
            var tag = await Dbset.Where(i=>i.Id==id).FirstOrDefaultAsync();
            return tag;
        }

        public async Task<Tag> GetObjectByNameAsync(string name)
        {
            var tag = await Dbset.Where(i => i.Name == name).FirstOrDefaultAsync();
            return tag;
        }

        public override Task SoftDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task SoftDeleteByIntIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Tag obj)
        {
            throw new NotImplementedException();
        }
    }
}
