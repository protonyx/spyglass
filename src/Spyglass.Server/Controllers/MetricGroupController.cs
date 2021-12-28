using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Server.Data;
using Spyglass.Server.DTO;
using Spyglass.Server.Models;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MetricGroupController : Controller
    {
        private IRepository<MetricGroup> MetricGroupRepository { get; }

        private IRepository<Metric> MetricRepository { get; }
        
        private IMapper Mapper { get; }

        public MetricGroupController(
            IRepository<MetricGroup> metricGroupRepository,
            IRepository<Metric> metricRepository,
            IMapper mapper)
        {
            MetricGroupRepository = metricGroupRepository;
            MetricRepository = metricRepository;
            Mapper = mapper;
        }

        /// <summary>
        /// Get all metric groups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<MetricGroupDTO>), (int) HttpStatusCode.OK)]
        public IActionResult Get()
        {
            var groups = this.MetricGroupRepository.GetAll()
                .Select(Mapper.Map<MetricGroupDTO>)
                .ToList();
            
            return Ok(groups);
        }

        /// <summary>
        /// Get a single metric group
        /// </summary>
        /// <param name="id">Metric group ID (Guid)</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MetricGroup), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get(Guid id)
        {
            var group = this.MetricGroupRepository.Get(id);

            if (group == null)
                return NotFound();

            var dto = Mapper.Map<MetricGroupDTO>(group);

            return Ok(dto);
        }

        /// <summary>
        /// Create a metric group
        /// </summary>
        /// <param name="dto">Group definition</param>
        [HttpPost]
        [ProducesResponseType(typeof(MetricGroup), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        public IActionResult Create([FromBody] MetricGroupDTO dto)
        {
            var existing = this.MetricGroupRepository
                .FindBy(t => t.Name.Equals(dto.Name))
                .FirstOrDefault();

            if (existing != null)
            {
                ModelState.AddModelError(nameof(MetricGroupDTO.Name), $"Metric group with name {dto.Name} already exists");
                return BadRequest(ModelState);
            }

            var group = Mapper.Map<MetricGroup>(dto);
            group.Id = Guid.NewGuid();
            this.MetricGroupRepository.Add(group);

            Mapper.Map(group, dto);
            
            return CreatedAtAction(nameof(Get), new { id = group.Id }, dto);
        }

        /// <summary>
        /// Delete a metric group
        /// </summary>
        /// <param name="id">Metric group ID (Guid)</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeleteGroup(Guid id)
        {
            var group = this.MetricGroupRepository.Get(id);

            if (group == null)
                return NotFound();

            var metrics = MetricRepository.FindBy(t => t.MetricGroupId == id);

            if (metrics.Any())
            {
                return BadRequest(new
                {
                    Message = "Unable to delete group that has associated metrics"
                });
            }

            this.MetricGroupRepository.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Get metrics associated with a metric group
        /// </summary>
        /// <param name="id">Metric group ID (Guid)</param>
        [HttpGet("{id}/Metrics")]
        [ProducesResponseType(typeof(ICollection<MetricDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMetrics(Guid id)
        {
            var group = this.MetricGroupRepository.Get(id);

            if (group == null)
                return NotFound();

            var metrics = this.MetricRepository
                .FindBy(t => t.MetricGroupId.Equals(id))
                .ToList()
                .Select(Mapper.Map<MetricDTO>);

            return Ok(metrics);
        }
    }
}
