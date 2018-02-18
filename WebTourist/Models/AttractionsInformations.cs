using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTourist.Models
{
    public class AttractionsInformations
    {
        public AttractionsInformations(string name,string des,string coord)
        {
            Name = name;
            Description = des;
            CoordinateOGC = coord;
        }
        public string Name { get; set; }

        public string Description { get; set; }
        public string CoordinateOGC { get; set; }
    }
}