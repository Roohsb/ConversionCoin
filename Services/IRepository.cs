using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conversion.Services
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll(Func<T, bool> filters = null);
        Task<T> InsertAsync(T item);
        Task<T> UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
