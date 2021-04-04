using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class UserAssignment
    {
        public int IdUserAssignment { get; set; }
        public int IdUser { get; set; }
        public int IdAssignment { get; set; }

        public virtual Assignment IdAssignmentNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
