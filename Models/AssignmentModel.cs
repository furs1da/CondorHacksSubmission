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
        public bool Done { get; set; }
        public string IdSubject { get; set; }
        public bool? Flagged { get; set; }
    }
}
