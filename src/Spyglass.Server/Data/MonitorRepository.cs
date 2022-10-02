using System;
using Spyglass.Server.Models;

namespace Spyglass.Server.Data
{
    public class MonitorRepository : EntityFrameworkRepository<Monitor>
    {
        public MonitorRepository(SpyglassDbContext dbContext) : base(dbContext)
        {
        }

        public override Monitor Add(Monitor entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;

            return base.Add(entity);
        }

        public override Monitor Update(Monitor entity)
        {
            entity.ModifiedDate = DateTime.Now;
            
            return base.Update(entity);
        }
    }
}