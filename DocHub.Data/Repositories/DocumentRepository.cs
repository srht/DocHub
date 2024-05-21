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

        public override void SoftDelete(Guid id)
        {
            var found=Dbset.Find(id);
            found.IsDeleted= true;
            Commit();
        }

        public override void SoftDeleteByIntId(int id)
        {
            var found = Dbset.Find(id);
            found.IsDeleted = true;
            Commit();
        }

        public override void Update(DDocument obj)
        {
            Dbset.Update(obj);
            Commit();
        }

        public void PatchDocument(DDocument obj)
        {
            Commit();
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
    }
}
