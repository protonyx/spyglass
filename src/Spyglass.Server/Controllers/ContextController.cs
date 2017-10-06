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

        public ContextController(IDataContext dataContext)
        {
            this.MetricContextRepository = dataContext.Repository<MetricContext>();
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

            return Ok(context.Metrics);
        }

        [HttpGet("{contextName}/Metrics/{id}")]
        public IActionResult GetMetric(string contextName, Guid id)
        {
            var context = GetContext(contextName);

            var metric = context.Metrics.FirstOrDefault(t => t.Id == id);

            if (metric == null)
                return NotFound();

            return Ok(metric);
        }

        [HttpPost("{contextName}/Metrics")]
        public IActionResult CreateMetric(string contextName, [FromBody]Metric value)
        {
            var context = GetContext(contextName);

            value.Id = Guid.NewGuid();
            context.Metrics.Add(value);

            this.MetricContextRepository.Update(context);

            return Ok(value);
        }

        //[HttpPut("{contextName}/Metrics/{id}")]
        //public IActionResult UpdateMetric(string contextName, Guid id, [FromBody]Metric entity)
        //{
        //    var uow = UnitOfWorkFactory.Create();

        //    uow.Repository<Metric>().Update(id, entity);
        //}

        [HttpDelete("{contextName}/Metrics/{id}")]
        public IActionResult DeleteMetric(string contextName, Guid id)
        {
            var context = GetContext(contextName);

            var metric = context.Metrics.FirstOrDefault(t => t.Id == id);

            if (metric == null)
                return NotFound();

            context.Metrics.Remove(metric);
            this.MetricContextRepository.Update(context);

            return NoContent();
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
