using System;

namespace Spyglass.Api.Models
{
    public class Metric
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int ModifiedUserId { get; set; }
    }
}