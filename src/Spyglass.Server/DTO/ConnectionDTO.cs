using System;
using System.ComponentModel.DataAnnotations;

namespace Spyglass.Server.DTO
{
    public class ConnectionDTO
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string DatabaseType { get; set; }
        
        public string ConnectionString { get; set; }
    }
}