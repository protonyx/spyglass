using System;
using System.ComponentModel.DataAnnotations;

namespace Spyglass.Server.DTO
{
    public class MetricDTO
    {
        public Guid? Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z_:][a-zA-Z0-9_:]*")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public Guid ConnectionId { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ModifiedDate { get; set; }

        [Required]
        public string Query { get; set; }
        
        public string Units { get; set; }
    }
}