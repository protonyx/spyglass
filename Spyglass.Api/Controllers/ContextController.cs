using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Api.DAL;
using Spyglass.Core;

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

        protected MongoRepository<MetricContext> GetRepository()
        {
            var uow = this.UnitOfWorkFactory.Create();
            return uow.Repository<MetricContext>();
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
    }
}