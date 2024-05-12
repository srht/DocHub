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
        void Delete(Guid id);
        void DeleteByIntId(int id);
        void SoftDelete(Guid id);
        void SoftDeleteByIntId(int id);
        void Update(T obj);
        bool Commit(bool state = true);
    }
}
