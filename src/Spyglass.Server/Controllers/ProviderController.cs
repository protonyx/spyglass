using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.SDK;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;
using Spyglass.SDK.Providers;
using Spyglass.SDK.Services;
using Spyglass.Server.Services;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        protected IMapper Mapper { get; }

        protected MetadataService MetadataService { get; }

        public ProviderController(
            IMapper mapper,
            MetadataService metadataService)
        {
            Mapper = mapper;
            MetadataService = metadataService;
        }

        /// <summary>
        /// Get all known providers and metadata
        /// </summary>
        /// <returns>Provider metadata</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SDK.Models.MetricProviderMetadata>), (int)HttpStatusCode.OK)]
        public IActionResult GetProviders()
        {
            var descriptors = ProviderService.GetProviders()
                .Select(kv => MetadataService.GetMetadata(kv.Value))
                .ToList();

            return Ok(descriptors);
        }

        /// <summary>
        /// Get provider metadata
        /// </summary>
        /// <param name="name">Provider type</param>
        /// <returns>Provider metadata</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(SDK.Models.MetricProviderMetadata), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetProvider(string name)
        {
            var provider = ProviderService.GetProvider(name);

            if (provider == null)
                return NotFound();

            var descriptor = MetadataService.GetMetadata(provider);

            return Ok(descriptor);
        }

        /// <summary>
        /// Test a provider's configuration
        /// </summary>
        /// <param name="name">Provider type</param>
        /// <param name="config">Configuration parameters</param>
        /// <returns>Metric values</returns>
        [HttpPost("{name}/Test")]
        [ProducesResponseType(typeof(ICollection<MetricValue>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> TestProviderAsync(string name, [FromBody] IDictionary<string, string> config)
        {
            var providerType = ProviderService.GetProvider(name);

            if (providerType == null)
                return NotFound();

            var provider = (IMetricValueProvider)Activator.CreateInstance(providerType);

            var providerProperties = providerType.GetProperties();
            foreach (var prop in providerProperties)
            {
                if (config.TryGetValue(prop.Name, out var value))
                {
                    prop.SetValue(provider, value);
                }
            }

            var val = await provider.GetValueAsync();

            return Ok(val);
        }
    }
}
