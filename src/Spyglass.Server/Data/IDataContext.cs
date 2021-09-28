namespace Spyglass.Server.Data
{
  public interface IDataContext
  {
    IRepository<TEntity> Repository<TEntity>() where TEntity : IHasKey;
  }
}
