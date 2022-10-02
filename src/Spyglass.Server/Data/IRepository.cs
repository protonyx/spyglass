using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Spyglass.Server.Data
{
    public interface IRepository<TModel> where TModel : IHasKey
    {
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> FindBy(Expression<Func<TModel, bool>> predicate);
        IEnumerable<TModel> FindBy(Func<IQueryable<TModel>, IEnumerable<TModel>> queryable);
        TModel Get(object key);
        TModel Add(TModel entity);
        TModel Update(TModel entity);
        void Delete(TModel entity);
        void Delete(object key);
    }
}
