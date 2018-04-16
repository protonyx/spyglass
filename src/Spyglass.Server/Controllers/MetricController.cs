using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MetricController : Controller
    {
        protected IRepository<Metric> MetricRepository { get; }

        public MetricController(
            IDataContext dataContext)
        {
            MetricRepository = dataContext.Repository<Metric>();
        }

        [HttpGet]
        public IActionResult GetMetrics()
        {
            return Ok(this.MetricRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetMetric(Guid id)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpGet("{id}/Value")]
        public IActionResult GetMetricValue(Guid id)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            var provider = entity.Provider;
            var value = provider.GetValueAsync();
            
            return Ok(value.Result);
        }

        [HttpPost]
        public IActionResult CreateMetric([FromBody] Metric entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;

            this.MetricRepository.Add(entity);

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMetric(Guid id, [FromBody] Metric metric)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            entity.ModifiedDate = DateTime.Now;

            this.MetricRepository.Update(metric);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMetric(Guid id)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            this.MetricRepository.Delete(entity);

            return NoContent();
        }
    }
}