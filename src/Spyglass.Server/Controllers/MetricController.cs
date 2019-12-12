using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;
using Spyglass.SDK.Services;
using Spyglass.Server.DTO;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MetricController : ControllerBase
    {
        private IRepository<Metric> MetricRepository { get; }
        
        private IMapper Mapper { get; }

        public MetricController(
            IDataContext dataContext,
            IMapper mapper)
        {
            Mapper = mapper;
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

            var provider = entity.Provider;
            var value = await provider.GetValueAsync();
            
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