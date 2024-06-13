using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Abstracts
{
    public interface IDocumentRepository:IGenericRepository<DDocument>
    {
        IQueryable<DDocument> QueryList(params string[] includes);
        IEnumerable<DDocument> GetList(params string[] includes);
        DDocument GetDocumentById(Guid id);
        Task<DDocument> GetDocumentByIdAsync(Guid id);
        Task InsertAsync(DDocument obj);
    }
}
