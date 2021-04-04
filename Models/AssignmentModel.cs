using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondorHacks.Models
{
    public class AssignmentModel
    {
        public int IdAssignment { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string Length { get; set; }
        public decimal PercentOfFinalGrade { get; set; }
        public string Done { get; set; }
        public string IdSubject { get; set; }
        public bool? Flagged { get; set; }
        public AssignmentModel(int IdAssignment, string Name, DateTime Deadline, string Description, string Difficulty, string Length, decimal PercentOfFinalGrade, string Done, string IdSubject, bool? Flagged)
        {
            this.IdAssignment = IdAssignment;
            this.Name = Name;
            this.Deadline = Deadline;
            this.Description = Description;
            this.Difficulty = Difficulty;
            this.Length = Length;
            this.PercentOfFinalGrade = PercentOfFinalGrade;
            this.Done = Done;
            this.IdSubject = IdSubject;
            this.Flagged = Flagged;
        }
    }
}
