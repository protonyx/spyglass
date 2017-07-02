namespace Spyglass.Server.DAL
{
    public interface IRepositoryFactory
    {
        IRepository<TModel> Create<TModel>();
    }
}