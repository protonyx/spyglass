using Spyglass.SDK.Data;

namespace Spyglass.Server.DAL
{
    public interface IRepositoryFactory
    {
        IRepository<TModel> Create<TModel>() where TModel : IHasKey;
    }
}