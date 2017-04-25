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

        protected MongoRepository<IMetricContext> GetRepository()
        {
            var uow = this.UnitOfWorkFactory.Create();
            return uow.Repository<IMetricContext>();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var repo = GetRepository();
            return Ok(repo.GetAll());
        }
    }
}