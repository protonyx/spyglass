using System;
using System.ComponentModel.DataAnnotations;
using Spyglass.Server.Data;

namespace Spyglass.Server.Models
{
    public class Monitor : IHasKey
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Category { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z_:][a-zA-Z0-9_:]*")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        public Guid ConnectionId { get; set; }

        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }
        
        [Required]
        public string Query { get; set; }
        
        public string Units { get; set; }
        
        public virtual DatabaseConnection Connection { get; set; }

        public object GetKey()
        {
            return Id;
        }
    }
}
