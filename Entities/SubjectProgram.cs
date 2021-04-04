using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class SubjectProgram
    {
        public int IdSubjectProgram { get; set; }
        public int IdSubject { get; set; }
        public int IdProgram { get; set; }

        public virtual ProgramName IdProgramNavigation { get; set; }
        public virtual Subjects IdSubjectNavigation { get; set; }
    }
}
