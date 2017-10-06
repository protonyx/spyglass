using System;

namespace Spyglass.SDK.Data
{
  public interface IDataContext
  {
    IRepository<TEntity> Repository<TEntity>() where TEntity : IHasKey;
  }
}
