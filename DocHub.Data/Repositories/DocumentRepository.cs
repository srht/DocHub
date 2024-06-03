using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Repositories
{
    public class DocumentRepository : EFCoreRepository<DDocument>, IDocumentRepository
    {
        public DocumentRepository(DocHubDbContext docHubDbContext) : base(docHubDbContext)
        {
            DocHubDbContext = docHubDbContext;
        }

        public DocHubDbContext DocHubDbContext { get; }

        public IEnumerable<DDocument> GetList(params string[] includes)
        {
            var listQuery = Dbset.AsQueryable();
            foreach (string include in includes)
            {
                listQuery = listQuery.Include(include);
            }

            return listQuery.ToList();
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var found=Dbset.Find(id);
            found.IsDeleted= true;
            await CommitAsync();
        }

        public override async Task SoftDeleteByIntIdAsync(int id)
        {
            var found = Dbset.Find(id);
            found.IsDeleted = true;
            await CommitAsync();
        }

        public async Task InsertAsync(DDocument obj)
        {
            Dbset.Add(obj);
            await CommitAsync();
        }
        public override async Task UpdateAsync(DDocument obj)
        {
            Dbset.Update(obj);
            await CommitAsync();
        }

        public DDocument GetDocumentById(Guid id)
        {
            var foundDoc= Dbset.Find(id);
            if(foundDoc==null)
                return null;
            Dbset.Entry(foundDoc).Collection(d => d.Categories).LoadAsync();
            Dbset.Entry(foundDoc).Collection(d => d.Tags).LoadAsync();
            return foundDoc;
        }

        public async Task<DDocument> GetDocumentByIdAsync(Guid id)
        {
            var foundDoc = Dbset.Find(id);
            if (foundDoc == null)
                return null;
           await Dbset.Entry(foundDoc).Collection(d => d.Categories).LoadAsync();
           await Dbset.Entry(foundDoc).Collection(d => d.Tags).LoadAsync();
            return foundDoc;
        }
    }
}
