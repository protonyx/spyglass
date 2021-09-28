using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Server.Data;
using Spyglass.Server.DTO;
using Spyglass.Server.Models;
using Spyglass.Server.Services;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MetricController : ControllerBase
    {
        private IRepository<Metric> MetricRepository { get; }
        
        private MetricsService MetricService { get; }
        
        private IMapper Mapper { get; }

        public MetricController(
            IDataContext dataContext,
            IMapper mapper, MetricsService metricService)
        {
            Mapper = mapper;
            MetricService = metricService;
            MetricRepository = dataContext.Repository<Metric>();
        }

        [HttpGet]
        public IActionResult GetMetrics()
        {
            var metrics = this.MetricRepository.GetAll()
                .Select(this.Mapper.Map<MetricDTO>)
                .ToList();
            
            return Ok(metrics);
        }

        [HttpGet("{id}")]
        public IActionResult GetMetric(Guid id)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            var dto = this.Mapper.Map<MetricDTO>(entity);

            return Ok(dto);
        }

        [HttpGet("{id}/Value")]
        public async Task<IActionResult> GetMetricValueAsync(Guid id)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            var value = await MetricService.GetMetricValue(entity);
            
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateMetric([FromBody] MetricDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var entity = this.Mapper.Map<Metric>(dto);
            
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;

            this.MetricRepository.Add(entity);

            this.Mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMetric(Guid id, [FromBody] MetricDTO dto)
        {
            var entity = this.MetricRepository
                .FindBy(t => t.Id.Equals(id))
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            entity.ModifiedDate = DateTime.Now;
            
            this.Mapper.Map(dto, entity);

            this.MetricRepository.Update(entity);
            
            this.Mapper.Map(entity, dto);

            return Ok(dto);
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