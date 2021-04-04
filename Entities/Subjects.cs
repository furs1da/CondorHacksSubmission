using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Subjects
    {
        public Subjects()
        {
            Assignment = new HashSet<Assignment>();
            SubjectProgram = new HashSet<SubjectProgram>();
            UserElective = new HashSet<UserElective>();
        }

        public int IdSubject { get; set; }
        public string SubjectName { get; set; }
        public bool IsElective { get; set; }
        public int? LevelSubject { get; set; }

        public virtual Levels LevelSubjectNavigation { get; set; }
        public virtual ICollection<Assignment> Assignment { get; set; }
        public virtual ICollection<SubjectProgram> SubjectProgram { get; set; }
        public virtual ICollection<UserElective> UserElective { get; set; }
    }
}
