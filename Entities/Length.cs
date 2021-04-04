using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Length
    {
        public Length()
        {
            Assignment = new HashSet<Assignment>();
        }

        public int IdLength { get; set; }
        public string Length1 { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
    }
}
