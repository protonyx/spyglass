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
            .CreateGauge("metrics_raw", "Raw metric value", "category", "name");

        private readonly IRepository<Monitor> _monitorRepository;

        private readonly IRepository<DatabaseConnection> _connectionRepository;

        public MetricsService(
            IRepository<Monitor> monitorRepository,
            IRepository<DatabaseConnection> connectionRepository)
        {
            _monitorRepository = monitorRepository;
            _connectionRepository = connectionRepository;
        }

        public Task<IMetricValue> GetMetricValue(Monitor monitor)
        {
            var conn = _connectionRepository.Get(monitor.ConnectionId);
            
            switch (conn.DatabaseType)
            {
                case "SqlServer":
                    var provider = new SqlServerMetricValueProvider();
                    return provider.GetValueAsync(monitor, conn);
            }
            
            var res = new MetricValue()
            {
                Name = monitor.Name,
                Units = monitor.Units,
                Value = 0.0
            };
            
            return Task.FromResult((IMetricValue) res);
        }

        public Task UpdateMetricsAsync()
        {
            var metrics = _monitorRepository.GetAll();
            
            var tasks = new List<Task>();

            foreach (var metric in metrics)
            {
                tasks.Add(GetMetricValue(metric).ContinueWith(ant =>
                {
                    var res = ant.Result;
                    if (res != null)
                    {
                        MetricsRaw.WithLabels(metric.Category ?? string.Empty, metric.Name).Set(res.Value);
                    }
                }));
            }

            return Task.WhenAll(tasks);
        }
    }
}