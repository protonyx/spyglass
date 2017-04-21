using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spyglass.Api.DAL;
using Spyglass.Api.Models;

namespace Spyglass.Api.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {
        protected MongoUnitOfWorkFactory UnitOfWorkFactory { get; }

        public MetricController(MongoUnitOfWorkFactory uowFactory)
        {
            this.UnitOfWorkFactory = uowFactory;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Metric> Get()
        {
            var uow = UnitOfWorkFactory.Create();

            return uow.Repository<Metric>().GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Metric Get(Guid id)
        {
            var uow = UnitOfWorkFactory.Create();

            return uow.Repository<Metric>().Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Metric value)
        {
            var uow = UnitOfWorkFactory.Create();

            uow.Repository<Metric>().Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]Metric entity)
        {
            var uow = UnitOfWorkFactory.Create();

            uow.Repository<Metric>().Update(id, entity);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var uow = UnitOfWorkFactory.Create();

            uow.Repository<Metric>().Remove(id);
        }
    }
}
