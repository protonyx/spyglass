using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Spyglass.Server.Data;
using Spyglass.Server.Models;
using Spyglass.Server.ValueObjects;

namespace Spyglass.Server.Providers
{
    public class SqlServerMetricValueProvider : IMetricValueProvider
    {

        public async Task<IMetricValue> GetValueAsync(Monitor monitor, DatabaseConnection connection)
        {
            var csb = new SqlConnectionStringBuilder(connection.ConnectionString)
            {
                ApplicationName = "Spyglass"
            };

            var dbConn = new SqlConnection(csb.ConnectionString);
            var res = await dbConn.ExecuteScalarAsync<double>(monitor.Query);

            return new MetricValue()
            {
                Name = monitor.Name,
                Units = monitor.Units,
                Value = res
            };
        }
    }
}