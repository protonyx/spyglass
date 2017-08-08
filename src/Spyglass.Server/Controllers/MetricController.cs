using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.SDK;
using Spyglass.SDK.Data;
using Spyglass.Server.DAL;
using Spyglass.Server.Models;

namespace Spyglass.Server.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {
        protected IMapper Mapper { get; }

        protected IRepositoryFactory RepositoryFactory { get; }

        public MetricController(
            IMapper mapper,
            IRepositoryFactory repositoryFactory)
        {
            Mapper = mapper;
            RepositoryFactory = repositoryFactory;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var repo = RepositoryFactory.Create<Metric>();

            return Ok(repo.GetAll());
        }

        [HttpPost]
        public IActionResult CreateMetric([FromBody] Metric metric)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var repo = RepositoryFactory.Create<Metric>();

            var newMetric = repo.Add(metric);
            return Created(Url.Action(nameof(Get)), newMetric);
        }
    }
}
