using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Spyglass.Server.Data;
using Spyglass.Server.Models;

namespace Spyglass.Server.Providers
{
    public class SqlServerMetricValueProvider : IMetricValueProvider
    {

        public async Task<IMetricValue> GetValueAsync(Metric metric, DatabaseConnection connection)
        {
            var csb = new SqlConnectionStringBuilder(connection.ConnectionString);
            var cs = csb.ToString();

            var dbConn = new SqlConnection(cs);
            var res = await dbConn.ExecuteScalarAsync<double>(metric.Query);

            return new MetricValue()
            {
                Name = metric.Name,
                Units = metric.Units,
                Value = res
            };
        }
    }
}