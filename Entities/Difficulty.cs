using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Difficulty
    {
        public Difficulty()
        {
            Assignment = new HashSet<Assignment>();
        }

        public int IdLevelOfDifficulty { get; set; }
        public int Value { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
    }
}
