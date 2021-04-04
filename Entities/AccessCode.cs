using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class AccessCode
    {
        public int IdCode { get; set; }
        public string AccessCode1 { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
