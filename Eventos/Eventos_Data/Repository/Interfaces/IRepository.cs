using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventos_Data.Repository.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T entity);
        Task<T> Read(Guid id);
        void Update(T entity);
        void Delete(Guid id);
        Task<List<T>> ReadAll();
    }
}
