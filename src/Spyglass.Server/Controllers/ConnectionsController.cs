using System;
using System.Linq;
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
    public class ConnectionsController : ControllerBase
    {

        private readonly IRepository<DatabaseConnection> _connectionRepository;

        private readonly IMapper _mapper;

        public ConnectionsController(
            IRepository<DatabaseConnection> connectionRepository,
            IMapper mapper)
        {
            _connectionRepository = connectionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetConnections()
        {
            var metrics = this._connectionRepository.GetAll()
                .Select(this._mapper.Map<ConnectionDTO>)
                .ToList();
            
            return Ok(metrics);
        }

        [HttpGet("{id}")]
        public IActionResult GetConnection(Guid id)
        {
            var entity = this._connectionRepository.Get(id);

            if (entity == null)
                return NotFound();

            var dto = this._mapper.Map<ConnectionDTO>(entity);

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateConnection([FromBody] ConnectionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var entity = this._mapper.Map<DatabaseConnection>(dto);
            
            entity.Id = Guid.NewGuid();

            this._connectionRepository.Add(entity);

            this._mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateConnection(Guid id, [FromBody] ConnectionDTO dto)
        {
            var entity = this._connectionRepository.Get(id);

            if (entity == null)
                return NotFound();
            
            this._mapper.Map(dto, entity);

            this._connectionRepository.Update(entity);
            
            this._mapper.Map(entity, dto);

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConnection(Guid id)
        {
            var entity = this._connectionRepository.Get(id);

            if (entity == null)
                return NotFound();

            this._connectionRepository.Delete(entity);

            return NoContent();
        }
    }
}