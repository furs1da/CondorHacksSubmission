using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CondorHacks.Models
{
    public class CreateAssignment 
    {
        [Required]
        public string NameAsg { get; set; } 

        [Required]
        public string Description { get; set; }
        [Required]
        public int DifficultyLevel { get; set; } 

        [Required]
        public int SubjectName { get; set; }
        [Required]
        public int LengthDur { get; set; } 

        [Required]
        public decimal Percentage { get; set; }
        [Required]
        public string Deadline { get; set; } 
    }
}
