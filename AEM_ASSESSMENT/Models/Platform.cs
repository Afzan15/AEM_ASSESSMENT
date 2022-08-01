using System;
using System.Collections.Generic;

namespace AEM_ASSESSMENT.Models
{
    public partial class Platform
    {
        public int Id { get; set; }
        public string? UniqueName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? CreatedAt{ get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual List<Well> Well { get; set; } = new List<Well>();

    }
}
