using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spyglass.Server.Data;

namespace Spyglass.Server.Models
{
    public class DatabaseConnection : IHasKey
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string Name { get; set; }
        
        public string DatabaseType { get; set; }
        
        public string ConnectionString { get; set; }
        
        public virtual ICollection<Monitor> Monitors { get; set; }
        
        public object GetKey()
        {
            return Id;
        }
    }
}