using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.Core;
using Spyglass.SDK.Metrics;
using Spyglass.Server.Models;

namespace Spyglass.Server.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {
        protected IMapper Mapper { get; }

        public MetricController(IMapper mapper)
        {
            Mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var assm = Assembly.Load(new AssemblyName("Spyglass.Core"));

            var metricTypes = assm.ExportedTypes
                .Where(t => typeof(IMetric).IsAssignableFrom(t))
                .Where(t => t.GetTypeInfo().GetCustomAttribute<ConfigurableMetricAttribute>() != null)
                .ToList();

            var modelExplorer = new EmptyModelMetadataProvider();

            var descriptors = metricTypes
                .Select(t => new MetricDescriptor
                {
                    Name = t.GetTypeInfo().GetCustomAttribute<ConfigurableMetricAttribute>().Name,
                    Properties = modelExplorer.GetMetadataForProperties(t)
                        .Select(this.Mapper.Map<ModelPropertyMetadata>)
                });

            return Ok(descriptors);
        }
    }
}
