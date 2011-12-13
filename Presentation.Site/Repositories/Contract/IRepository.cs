using System;
using System.Linq;
using System.Linq.Expressions;

namespace Projector.Site.Repositories.Contract
{
    public interface IRepository<TModel>
        where TModel : class, new()
    {
        void Add(TModel model);
        void Update(TModel model);
        void Delete(TModel model);
        TModel FindOne(Expression<Func<TModel, bool>> expression);
        IQueryable<TModel> All();
    }
}