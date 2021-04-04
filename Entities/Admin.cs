using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Admin
    {
        public int IdAdmin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int IdRole { get; set; }

        public virtual Roles IdRoleNavigation { get; set; }
    }
}
