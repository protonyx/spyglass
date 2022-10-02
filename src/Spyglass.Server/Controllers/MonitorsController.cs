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
    public class MonitorsController : ControllerBase
    {
        private readonly IRepository<Monitor> _monitorRepository;

        private readonly MetricsService _metricsService;

        private readonly IMapper _mapper;

        public MonitorsController(
            IRepository<Monitor> monitorRepository,
            IMapper mapper,
            MetricsService metricsService)
        {
            _monitorRepository = monitorRepository;
            _mapper = mapper;
            _metricsService = metricsService;
        }

        [HttpGet]
        public IActionResult GetMetrics()
        {
            var metrics = this._monitorRepository.GetAll()
                .Select(this._mapper.Map<MonitorDTO>)
                .ToList();
            
            return Ok(metrics);
        }

        [HttpGet("{id}")]
        public IActionResult GetMetric(Guid id)
        {
            var entity = this._monitorRepository.Get(id);

            if (entity == null)
                return NotFound();

            var dto = this._mapper.Map<MonitorDTO>(entity);

            return Ok(dto);
        }

        [HttpGet("{id}/Value")]
        public async Task<IActionResult> GetMetricValueAsync(Guid id)
        {
            var entity = this._monitorRepository.Get(id);

            if (entity == null)
                return NotFound();

            var value = await _metricsService.GetMetricValue(entity);
            
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateMetric([FromBody] MonitorDTO dto)
        {
            var entity = this._mapper.Map<Monitor>(dto);

            this._monitorRepository.Add(entity);

            this._mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMetric(Guid id, [FromBody] MonitorDTO dto)
        {
            var entity = this._monitorRepository.Get(id);

            if (entity == null)
                return NotFound();

            this._mapper.Map(dto, entity);

            this._monitorRepository.Update(entity);
            
            this._mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMetric(Guid id)
        {
            var entity = this._monitorRepository.Get(id);

            if (entity == null)
                return NotFound();

            this._monitorRepository.Delete(entity);

            return NoContent();
        }
    }
}