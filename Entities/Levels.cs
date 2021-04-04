using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class Levels
    {
        public Levels()
        {
            Subjects = new HashSet<Subjects>();
            Users = new HashSet<Users>();
        }

        public int IdLevel { get; set; }
        public int LevelValue { get; set; }

        public virtual ICollection<Subjects> Subjects { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
