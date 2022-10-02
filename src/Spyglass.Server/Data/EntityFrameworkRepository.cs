using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Spyglass.Server.Data
{
    public class EntityFrameworkRepository<TModel> : IRepository<TModel> where TModel : class, IHasKey
    {
        private DbContext DbContext { get; }
        private DbSet<TModel> DbSet { get; }

        public EntityFrameworkRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TModel>();
        }

        public IEnumerable<TModel> GetAll()
        {
            return DbSet.AsEnumerable();
        }

        public IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            return DbSet.AsQueryable().Where(predicate).AsEnumerable();
        }

        public IEnumerable<TModel> FindBy(Func<IQueryable<TModel>, IEnumerable<TModel>> queryable)
        {
            var q = DbSet.AsQueryable();
            return queryable(q).AsEnumerable();
        }

        public TModel Get(object key)
        {
            return DbSet.Find(key);
        }

        public TModel Add(TModel entity)
        {
            DbSet.Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public TModel Update(TModel entity)
        {
            DbSet.Update(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public void Delete(TModel entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }

        public void Delete(object key)
        {
            var entity = DbSet.Find(key);
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }
    }
}