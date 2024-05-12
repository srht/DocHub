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
    }
}
