using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Abstracts
{
    public interface ITagsRepository:IGenericRepository<Tag>
    {
        Task<Tag> GetObjectByIntIdAsync(int id);
    }
}
