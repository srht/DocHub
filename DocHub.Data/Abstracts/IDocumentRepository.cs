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
        IEnumerable<DDocument> GetList(params string[] includes);
    }
}
