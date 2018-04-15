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
using Spyglass.Server.Models;
using Spyglass.Server.Services;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProviderController : Controller
    {
        protected IMapper Mapper { get; }

        protected ProviderService ProviderService { get; }

        public ProviderController(
            IMapper mapper,
            ProviderService providerService)
        {
            Mapper = mapper;
            ProviderService = providerService;
        }

        /// <summary>
        /// Get all known providers and metadata
        /// </summary>
        /// <returns>Provider metadata</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ProviderDescriptor>), (int)HttpStatusCode.OK)]
        public IActionResult GetProviders()
        {
            var descriptors = ProviderService.GetProviders()
                .Select(kv => ProviderService.GetMetadata(kv.Value))
                .ToList();

            return Ok(descriptors);
        }

        /// <summary>
        /// Get provider metadata
        /// </summary>
        /// <param name="name">Provider type</param>
        /// <returns>Provider metadata</returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(ProviderDescriptor), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetProvider(string name)
        {
            var provider = ProviderService.GetProvider(name);

            if (provider == null)
                return NotFound();

            var descriptor = ProviderService.GetMetadata(provider);

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
