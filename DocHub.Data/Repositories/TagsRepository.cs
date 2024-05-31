using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
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
