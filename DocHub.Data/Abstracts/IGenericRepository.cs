using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Data.Abstracts
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetObjectById(Guid id);
        T GetObjectByIntId(int id);
        void Insert(T obj);
        Task DeleteAsync(Guid id);
        Task DeleteByIntIdAsync(int id);
        Task SoftDeleteAsync(Guid id);
        Task SoftDeleteByIntIdAsync(int id);
        Task UpdateAsync(T obj);
        Task<bool> CommitAsync(bool state = true);
    }
}
