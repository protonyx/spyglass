using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Api.DAL;
using Spyglass.Api.Models;
using Spyglass.Core;
using Spyglass.Core.Metrics;

namespace Spyglass.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContextController : Controller
    {
        protected MongoUnitOfWorkFactory UnitOfWorkFactory { get; }

        public ContextController(MongoUnitOfWorkFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var repo = GetRepository();
            return Ok(repo.GetAll());
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var repo = GetRepository();
            var context = repo.GetAll()
                .FirstOrDefault(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (context == null)
                return NotFound();

            return Ok(context);
        }

        [HttpPost("{name}")]
        public IActionResult Create(string name)
        {
            var repo = GetRepository();
            var existing = repo.GetAll()
                .FirstOrDefault(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (existing != null)
                return BadRequest();

            var context = new MetricContext
            {
                Name = name
            };

            repo.Add(context);
            return Ok(context);
        }

        // GET api/values
        [HttpGet("{contextName}/Metrics")]
        public IActionResult GetMetrics(string contextName)
        {
            var context = GetContext(contextName);

            return Ok(context.Metrics);
        }

        // GET api/values/5
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
        public IActionResult CreateMetric(string contextName, [FromBody]IMetric value)
        {
            var repo = GetRepository();
            var context = GetContext(contextName);

            value.Id = Guid.NewGuid();
            context.Metrics.Add(value);

            repo.Update(context.Id, context);

            return Ok(value);
        }
        
        //[HttpPut("{contextName}/Metrics/{id}")]
        //public IActionResult UpdateMetric(string contextName, Guid id, [FromBody]IMetric entity)
        //{
        //    var uow = UnitOfWorkFactory.Create();

        //    uow.Repository<Metric>().Update(id, entity);
        //}
        
        [HttpDelete("{contextName}/Metrics/{id}")]
        public IActionResult DeleteMetric(string contextName, Guid id)
        {
            var repo = GetRepository();
            var context = GetContext(contextName);

            var metric = context.Metrics.FirstOrDefault(t => t.Id == id);

            if (metric == null)
                return NotFound();

            context.Metrics.Remove(metric);
            repo.Update(context.Id, context);

            return NoContent();
        }

        protected MongoRepository<MetricContext> GetRepository()
        {
            var uow = this.UnitOfWorkFactory.Create();
            return uow.Repository<MetricContext>();
        }

        private MetricContext GetContext(string name)
        {
            var repo = GetRepository();
            var context = repo.GetAll()
                .FirstOrDefault(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            return context;
        }
    }
}