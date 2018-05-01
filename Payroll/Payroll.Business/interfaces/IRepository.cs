using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Business.interfaces
{
    public interface IRepository<T> where T : IStandardIdentity
    {
        Task<T> CreateAsync(T item);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> filterFunc);
        Task<IPaginatedList<T>> GetAllPagedAsync(Func<IQueryable<T>, IQueryable<T>> filterFunc, int pageIndex, int pageSize);
        Task<T> UpdateAsync(T item);
        Task<IEnumerable<T>> UpdateManyAsync(IEnumerable<T> items);
        Task DeleteAsync(T item);
        Task DeleteManyAsnyc(IEnumerable<T> items);
    }
}