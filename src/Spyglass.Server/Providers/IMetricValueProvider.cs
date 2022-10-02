using System.Threading.Tasks;
using Spyglass.Server.Data;
using Spyglass.Server.Models;

namespace Spyglass.Server.Providers
{
    public interface IMetricValueProvider
    {
        Task<IMetricValue> GetValueAsync(Monitor monitor, DatabaseConnection connection);
    }
}
