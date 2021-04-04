using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondorHacks.Models
{
    public class SelectModel
    {
        public string value { get; set; }
        public string label { get; set; }
        public SelectModel(string value, string label)
        {
            this.value = value;
            this.label = label;
        }
    }
}
