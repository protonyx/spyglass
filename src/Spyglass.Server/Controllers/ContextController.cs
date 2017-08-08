using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Spyglass.SDK.Data;
using Spyglass.Server.DAL;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ContextController : Controller
    {
        protected IRepositoryFactory RepositoryFactory { get; }

        public ContextController(IRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
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
            var context = repo.FindBy(t => t.Name.Equals(name))
                .FirstOrDefault();

            if (context == null)
                return NotFound();

            return Ok(context);
        }

        [HttpPost("{name}")]
        public IActionResult Create(string name)
        {
            var repo = GetRepository();
            var existing = repo.FindBy(t => t.Name.Equals(name))
                .FirstOrDefault();
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
        public IActionResult CreateMetric(string contextName, [FromBody]Metric value)
        {
            var repo = GetRepository();
            var context = GetContext(contextName);

            value.Id = Guid.NewGuid();
            context.Metrics.Add(value);

            repo.Update(context);

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
            var repo = GetRepository();
            var context = GetContext(contextName);

            var metric = context.Metrics.FirstOrDefault(t => t.Id == id);

            if (metric == null)
                return NotFound();

            context.Metrics.Remove(metric);
            repo.Update(context);

            return NoContent();
        }

        protected IRepository<MetricContext> GetRepository()
        {
            return this.RepositoryFactory.Create<MetricContext>();
        }

        private MetricContext GetContext(string name)
        {
            var repo = GetRepository();
            var context = repo.FindBy(q => q.Where(t => t.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                .FirstOrDefault();

            return context;
        }
    }
}