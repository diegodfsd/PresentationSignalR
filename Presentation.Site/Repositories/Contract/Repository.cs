using System;
using System.Linq;
using System.Linq.Expressions;

namespace Projector.Site.Repositories.Contract
{
    public class Repository<TModel> : IRepository<TModel>
        where TModel : class, new()
    {
        private readonly ISession session;

        public Repository()
        {
            session = new MongoSession();
        }

        public void Add(TModel model)
        {
            Execute(() => session.Add(model));
        }

        public void Update(TModel model)
        {
            Execute(() => session.Update(model));
        }

        public void Delete(TModel model)
        {
            Execute(() => session.Delete(model));
        }

        public TModel FindOne(Expression<Func<TModel, bool>> expression)
        {
            return session.All<TModel>().SingleOrDefault(expression);
        }

        public IQueryable<TModel> All()
        {
            return session.All<TModel>();
        }

        private void Execute(Action @action)
        {
            using (session)
            {
                @action();
            }
        }
    }
}