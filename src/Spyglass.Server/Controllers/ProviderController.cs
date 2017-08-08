using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.SDK;
using Spyglass.SDK.Data;
using Spyglass.SDK.Providers;
using Spyglass.Server.Models;

namespace Spyglass.Server.Controllers
{
    [Route("api/[controller]")]
    public class ProviderController : Controller
    {
        protected IMapper Mapper { get; }

        public ProviderController(IMapper mapper)
        {
            Mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var assm = Assembly.Load(new AssemblyName("Spyglass.Core"));

            var providerTypes = assm.ExportedTypes
                .Where(t => typeof(IMetricValueProvider).IsAssignableFrom(t))
                .Where(t => t.GetTypeInfo().GetCustomAttribute<ConfigurableMetricAttribute>() != null)
                .ToList();

            var modelExplorer = new EmptyModelMetadataProvider();

            var descriptors = providerTypes
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