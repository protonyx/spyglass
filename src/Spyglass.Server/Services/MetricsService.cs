using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Spyglass.Server.Data;
using Spyglass.Server.Models;
using Spyglass.Server.Providers;
using Spyglass.Server.ValueObjects;

namespace Spyglass.Server.Services
{
    public class MetricsService
    {
        private static readonly Gauge MetricsRaw = Metrics
            .CreateGauge("metrics_raw", "Raw metric value", "group", "name");

        private DbContext DbContext { get; }
        
        private IRepository<Metric> MetricRepository { get; }

        private IRepository<DatabaseConnection> ConnectionRepository { get; }

        public MetricsService(
            SpyglassDbContext dbContext,
            IRepository<Metric> metricRepository,
            IRepository<DatabaseConnection> connectionRepository)
        {
            DbContext = dbContext;
            MetricRepository = metricRepository;
            ConnectionRepository = connectionRepository;
        }

        public Task<IMetricValue> GetMetricValue(Metric metric)
        {
            var conn = ConnectionRepository.Get(metric.ConnectionId);
            
            switch (conn.DatabaseType)
            {
                case "SqlServer":
                    var provider = new SqlServerMetricValueProvider();
                    return provider.GetValueAsync(metric, conn);
            }
            
            var res = new MetricValue()
            {
                Name = metric.Name,
                Units = metric.Units,
                Value = 0.0
            };
            
            return Task.FromResult((IMetricValue) res);
        }

        public Task UpdateMetricsAsync()
        {
            var metrics = DbContext.Set<Metric>()
                .Include(t => t.Group)
                .ToList();
            
            var tasks = new List<Task>();

            foreach (var metric in metrics)
            {
                tasks.Add(GetMetricValue(metric).ContinueWith(ant =>
                {
                    var res = ant.Result;
                    if (res != null)
                    {
                        MetricsRaw.WithLabels(metric.Group?.Name ?? string.Empty, metric.Name).Set(res.Value);
                    }
                }));
            }

            return Task.WhenAll(tasks);
        }
    }
}