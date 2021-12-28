using System;
using System.ComponentModel.DataAnnotations;

namespace Spyglass.Server.DTO
{
    public class MetricGroupDTO
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}