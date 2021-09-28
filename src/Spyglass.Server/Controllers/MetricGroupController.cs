using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Server.Data;
using Spyglass.Server.Models;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MetricGroupController : Controller
    {
        protected IRepository<MetricGroup> MetricGroupRepository { get; }

        protected IRepository<Metric> MetricRepository { get; }

        public MetricGroupController(
            IDataContext dataContext)
        {
            this.MetricGroupRepository = dataContext.Repository<MetricGroup>();
            this.MetricRepository = dataContext.Repository<Metric>();
        }

        /// <summary>
        /// Get all metric groups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<MetricGroup>), (int) HttpStatusCode.OK)]
        public IActionResult Get()
        {
            return Ok(this.MetricGroupRepository.GetAll());
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
            var group = GetGroup(id);

            if (group == null)
                return NotFound();

            return Ok(group);
        }

        /// <summary>
        /// Create a metric group
        /// </summary>
        /// <param name="group">Group definition</param>
        [HttpPost]
        [ProducesResponseType(typeof(MetricGroup), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        public IActionResult Create([FromBody] MetricGroup group)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = this.MetricGroupRepository
                .FindBy(t => t.Name.Equals(group.Name))
                .FirstOrDefault();
          
            if (existing != null)
                return BadRequest($"Metric group with name {group.Name} already exists");

            if (!group.Id.HasValue || group.Id == Guid.Empty)
                group.Id = Guid.NewGuid();

            this.MetricGroupRepository.Add(group);
            return CreatedAtAction(nameof(Get), new { name = group.Name }, group);
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
            var group = GetGroup(id);

            if (group == null)
                return NotFound();

            this.MetricGroupRepository.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Get metrics associated with a metric group
        /// </summary>
        /// <param name="id">Metric group ID (Guid)</param>
        [HttpGet("{id}/Metrics")]
        [ProducesResponseType(typeof(ICollection<Metric>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMetrics(Guid id)
        {
            var group = GetGroup(id);

            if (group == null)
                return NotFound();

            // var metrics = this.MetricRepository
            //     .FindBy(t => t.MetricGroupId.Equals(group.Id))
            //     .ToList();

            return Ok();
        }

        /// <summary>
        /// Get metric values for all metrics in the group
        /// </summary>
        /// <param name="id">Metric group ID (Guid)</param>
        /// <returns></returns>
        [HttpGet("{id}/Export")]
        [ProducesResponseType(typeof(ICollection<MetricValue>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public IActionResult GetGroupValues(Guid id)
        {
            var group = GetGroup(id);

            if (group == null)
                return NotFound();

            // var metrics = this.MetricRepository
            //     .FindBy(t => t.MetricGroupId.Equals(group.Id))
            //     .ToList();

            return Ok();
        }

        private MetricGroup GetGroup(Guid id)
        {
            var context = this.MetricGroupRepository
                .FindBy(t => t.Id == id)
                .FirstOrDefault();

            return context;
        }
    }
}
