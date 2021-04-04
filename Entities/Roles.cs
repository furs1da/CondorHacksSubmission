using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            Admin = new HashSet<Admin>();
            Users = new HashSet<Users>();
        }

        public int IdRole { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Admin> Admin { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
