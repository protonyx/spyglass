using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.Api.DAL;
using Spyglass.Api.Models;
using Spyglass.Core;
using Spyglass.Core.Metrics;

namespace Spyglass.Api.Controllers
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
