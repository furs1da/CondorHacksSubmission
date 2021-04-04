using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondorHacks.Models
{
    public class SelectModelSearch
    {
        public string value { get; set; }
        public string name { get; set; }
        public SelectModelSearch(string value, string name)
        {
            this.value = value;
            this.name = name;
        }
    }
}
