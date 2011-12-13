using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Norm;
using Norm.Collections;
using Norm.Linq;
using Norm.Responses;

namespace Projector.Site.Repositories.Contract
{
    //Reference:http://normproject.org/samples

    public class MongoSession : ISession
    {
        private readonly IMongo _provider;
        public IMongoDatabase Db { get { return _provider.Database; } }

        public MongoSession()
        {
            _provider = Mongo.Create("mongodb://localhost/Presentations?strict=false");
        }

        public void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            var items = All<T>().Where(expression);
            foreach (T item in items)
            {
                Delete(item);
            }
        }

        public void Delete<T>(T item) where T : class, new()
        {
            Db.GetCollection<T>().Delete(item);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            Db.DropCollection(typeof(T).Name);
        }

        public T Single<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return _provider.GetCollection<T>().AsQueryable();
        }

        public void Add<T>(T item) where T : class, new()
        {
            Db.GetCollection<T>().Insert(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public void Update<T>(T item) where T : class, new()
        {
            Db.GetCollection<T>().UpdateOne(item, item);
        }

        //Helper for using map reduce in mongo
        public T MapReduce<T>(string map, string reduce)
        {
            T result = default(T);
            MapReduce mr = Db.CreateMapReduce();

            MapReduceResponse response =
                mr.Execute(new MapReduceOptions(typeof(T).Name)
                {
                    Map = map,
                    Reduce = reduce
                });
            IMongoCollection<MapReduceResult<T>> coll = response.GetCollection<MapReduceResult<T>>();
            MapReduceResult<T> r = coll.Find().FirstOrDefault();
            result = r.Value;

            return result;
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}