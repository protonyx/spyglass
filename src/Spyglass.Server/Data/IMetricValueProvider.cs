using System.Collections.Generic;
using System.Threading.Tasks;
using Spyglass.Server.Models;

namespace Spyglass.Server.Data
{
    public interface IMetricValueProvider
    {
        Task<IMetricValue> GetValueAsync(Metric metric, DatabaseConnection connection);
    }
}
