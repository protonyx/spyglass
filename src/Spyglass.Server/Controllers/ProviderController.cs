using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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
    public class ProviderController : Controller
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
        [HttpPost("{name}")]
        [ProducesResponseType(typeof(ICollection<MetricValue>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        public IActionResult TestProvider(string name, [FromBody] IDictionary<string, string> config)
        {
            return Ok();
        }
    }
}
