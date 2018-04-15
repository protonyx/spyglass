using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContextController : Controller
    {
        protected IRepository<MetricContext> MetricContextRepository { get; }

        protected IRepository<Metric> MetricRepository { get; }

        public ContextController(
            IDataContext dataContext)
        {
            this.MetricContextRepository = dataContext.Repository<MetricContext>();
            this.MetricRepository = dataContext.Repository<Metric>();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.MetricContextRepository.GetAll());
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var context = this.MetricContextRepository
                .FindBy(t => t.Name.Equals(name))
                .FirstOrDefault();

            if (context == null)
                return NotFound();

            return Ok(context);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MetricContext entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Name))
                return BadRequest();
            
            var existing = this.MetricContextRepository
                .FindBy(t => t.Name.Equals(entity.Name))
                .FirstOrDefault();
          
            if (existing != null)
                return BadRequest();

            var created = this.MetricContextRepository.Add(entity);
            return Created(this.Url.Action(nameof(GetByName), new
            {
                name = entity.Name
            }), created);
        }

        [HttpGet("{contextName}/Metrics")]
        public IActionResult GetMetrics(string contextName)
        {
            var context = GetContext(contextName);

            if (context == null)
                return NotFound();

            var metrics = this.MetricRepository
                .FindBy(t => t.ContextId.Equals(context.Id))
                .ToList();

            return Ok(metrics);
        }

        private MetricContext GetContext(string name)
        {
            var context = this.MetricContextRepository
                .FindBy(q => q.Where(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                .FirstOrDefault();

            return context;
        }
    }
}
