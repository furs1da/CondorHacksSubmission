using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CondorHacks.Models
{
    public class RegistrationModel
    {
        [Required]
        public string FullName { get; set; } //e-mail

        [Required]
        public string PasswordUser { get; set; }
        [Required]
        public string EmailUser { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int ProgName { get; set; }
        [Required]
        public string AccessCode { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
    }
}
