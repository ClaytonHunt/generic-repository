using System.Linq;

namespace Payroll.Business.interfaces
{
    public interface IMapper<TModel, TEntity>
    {
        TModel SingleTo(TEntity item);
        IQueryable<TModel> ManyTo(IQueryable<TEntity> items);
        TEntity SingleFrom(TModel item);
        IQueryable<TEntity> WithIncludes(IQueryable<TEntity> items);
    }
}