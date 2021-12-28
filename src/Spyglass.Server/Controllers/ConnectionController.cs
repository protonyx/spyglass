using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Server.Data;
using Spyglass.Server.DTO;
using Spyglass.Server.Models;

namespace Spyglass.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        
        protected IRepository<DatabaseConnection> ConnectionRepository { get; }
        
        protected IMapper Mapper { get; }

        public ConnectionController(
            IRepository<DatabaseConnection> connectionRepository,
            IMapper mapper)
        {
            ConnectionRepository = connectionRepository;
            Mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetConnections()
        {
            var metrics = this.ConnectionRepository.GetAll()
                .Select(this.Mapper.Map<ConnectionDTO>)
                .ToList();
            
            return Ok(metrics);
        }

        [HttpGet("{id}")]
        public IActionResult GetConnection(Guid id)
        {
            var entity = this.ConnectionRepository.Get(id);

            if (entity == null)
                return NotFound();

            var dto = this.Mapper.Map<ConnectionDTO>(entity);

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateConnection([FromBody] ConnectionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var entity = this.Mapper.Map<DatabaseConnection>(dto);
            
            entity.Id = Guid.NewGuid();

            this.ConnectionRepository.Add(entity);

            this.Mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateConnection(Guid id, [FromBody] MetricDTO dto)
        {
            var entity = this.ConnectionRepository.Get(id);

            if (entity == null)
                return NotFound();
            
            this.Mapper.Map(dto, entity);

            this.ConnectionRepository.Update(entity);
            
            this.Mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConnection(Guid id)
        {
            var entity = this.ConnectionRepository.Get(id);

            if (entity == null)
                return NotFound();

            this.ConnectionRepository.Delete(entity);

            return NoContent();
        }
    }
}