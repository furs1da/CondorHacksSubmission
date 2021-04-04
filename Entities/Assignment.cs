using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Assignment
    {
        public Assignment()
        {
            UserAssignment = new HashSet<UserAssignment>();
        }

        public int IdAssignment { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public int Length { get; set; }
        public decimal PercentOfFinalGrade { get; set; }
        public bool Done { get; set; }
        public int IdSubject { get; set; }
        public bool? Flagged { get; set; }

        public virtual Difficulty DifficultyNavigation { get; set; }
        public virtual Subjects IdSubjectNavigation { get; set; }
        public virtual Length LengthNavigation { get; set; }
        public virtual ICollection<UserAssignment> UserAssignment { get; set; }
    }
}
