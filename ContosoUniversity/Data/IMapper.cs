using System.Linq;

namespace ContosoUniversity.Pages.Students
{
    public interface IMapper<TModel, TEntity>
    {
        TModel SingleTo(TEntity item);
        IQueryable<TModel> ManyTo(IQueryable<TEntity> items);
        TEntity SingleFrom(TModel item);
    }
}