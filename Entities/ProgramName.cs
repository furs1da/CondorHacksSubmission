using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class ProgramName
    {
        public ProgramName()
        {
            SubjectProgram = new HashSet<SubjectProgram>();
            Users = new HashSet<Users>();
        }

        public int IdProgram { get; set; }
        public string ProgramName1 { get; set; }

        public virtual ICollection<SubjectProgram> SubjectProgram { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
