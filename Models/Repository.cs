using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tsukaba.Models
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
        Task Save();
    }
}