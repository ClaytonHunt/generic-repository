using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Students
{
    public class GenericRepository<TModel, TEntity> : IRepository<TModel> where TEntity : class where TModel : IStandardIdentity
    {
        private readonly DbContext _context;
        private readonly IMapper<TModel, TEntity> _mapper;

        private IQueryable<TModel> Data => _mapper.ManyTo(_context.Set<TEntity>().AsQueryable());

        public GenericRepository(DbContext context, IMapper<TModel, TEntity> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TModel> CreateAsync(TModel item)
        {
            var newItem = _context.Add(_mapper.SingleFrom(item));

            await _context.SaveChangesAsync();

            return _mapper.SingleTo(newItem.Entity);
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            return await Data.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(
            Func<IQueryable<TModel>, IQueryable<TModel>> filterFunc)
        {
            filterFunc = filterFunc ?? (s => s);

            return await filterFunc(Data).ToListAsync();
        }

        public async Task<PaginatedList<TModel>> GetAllPagedAsync(Func<IQueryable<TModel>, IQueryable<TModel>> filterFunc, int pageIndex, int pageSize)
        {
            filterFunc = filterFunc ?? (s => s);

            var items = filterFunc(Data);

            return await PaginatedList<TModel>.CreateAsync(items, pageIndex, pageSize);
        }

        public async Task<TModel> UpdateAsync(TModel item)
        {
            var updatedItem = _context.Update(_mapper.SingleFrom(item));

            await _context.SaveChangesAsync();

            return _mapper.SingleTo(updatedItem.Entity);
        }

        public async Task<IEnumerable<TModel>> UpdateManyAsync(IEnumerable<TModel> items)
        {
            var itemsToUpdate = items.ToList();

            _context.UpdateRange(itemsToUpdate.Select(_mapper.SingleFrom));

            await _context.SaveChangesAsync();

            return itemsToUpdate;
        }

        public async Task DeleteAsync(TModel item)
        {
            _context.Remove(_mapper.SingleFrom(item));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteManyAsnyc(IEnumerable<TModel> items)
        {
            _context.RemoveRange(items.Select(_mapper.SingleFrom));

            await _context.SaveChangesAsync();
        }
    }
}