using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    public interface IGenericRepository<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(Guid id);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
    }
}
