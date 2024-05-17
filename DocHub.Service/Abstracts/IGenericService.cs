using DocHub.Common.DTO;
using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Abstracts
{
    public interface IGenericService<T> where T : IDataTransfer
    {
        List<T> GetList();
        T Get(Guid id);
        void Add(T objDto);
        void Update(T objDto);
        void Delete(Guid id);
        
    }
}
