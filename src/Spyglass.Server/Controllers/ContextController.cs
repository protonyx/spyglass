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
        public IActionResult Get(string name)
        {
            var context = this.MetricContextRepository
                .FindBy(t => t.Name.Equals(name))
                .FirstOrDefault();

            if (context == null)
                return NotFound();

            return Ok(context);
        }

        [HttpPost("{name}")]
        public IActionResult Create(string name)
        {
            var existing = this.MetricContextRepository
                .FindBy(t => t.Name.Equals(name))
                .FirstOrDefault();
          
            if (existing != null)
                return BadRequest();

            var context = new MetricContext
            {
                Name = name
            };

            this.MetricContextRepository.Add(context);
            return Ok(context);
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
