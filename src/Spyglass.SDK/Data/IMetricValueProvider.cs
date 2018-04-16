using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Data
{
    public interface IMetricValueProvider
    {
        Task<IEnumerable<IMetricValue>> GetValueAsync();

        string GetTypeName();
    }
}
