using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Spyglass.Server.Data
{
    public class EntityFrameworkRepository<TModel> : IRepository<TModel> where TModel : class, IHasKey
    {
        protected SpyglassDbContext _dbContext;
        
        protected  DbSet<TModel> _dbSet;

        public EntityFrameworkRepository(SpyglassDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TModel>();
        }

        public IEnumerable<TModel> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate)
        {
            return _dbSet.AsQueryable().Where(predicate).AsEnumerable();
        }

        public IEnumerable<TModel> FindBy(Func<IQueryable<TModel>, IEnumerable<TModel>> queryable)
        {
            var q = _dbSet.AsQueryable();
            return queryable(q).AsEnumerable();
        }

        public virtual TModel Get(object key)
        {
            return _dbSet.Find(key);
        }

        public virtual TModel Add(TModel entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual TModel Update(TModel entity)
        {
            _dbSet.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(TModel entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual void Delete(object key)
        {
            var entity = _dbSet.Find(key);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}