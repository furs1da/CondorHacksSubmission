using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Users
    {
        public Users()
        {
            UserAssignment = new HashSet<UserAssignment>();
            UserElective = new HashSet<UserElective>();
        }

        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public int ProgramId { get; set; }
        public int LevelProg { get; set; }
        public string Token { get; set; }
        public int IdRole { get; set; }

        public virtual Roles IdRoleNavigation { get; set; }
        public virtual Levels LevelProgNavigation { get; set; }
        public virtual ProgramName Program { get; set; }
        public virtual ICollection<UserAssignment> UserAssignment { get; set; }
        public virtual ICollection<UserElective> UserElective { get; set; }
    }
}
