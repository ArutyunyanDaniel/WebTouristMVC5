using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTourist.Models
{
    public class ContainerRouteAttractions
    {
        public ContainerRouteAttractions()
        {

            Attractions = new List<Attractions>();
        }

        public int IdR { get; set; }
        public string CoordinatesRoute { get; set; }
        public List<Attractions> Attractions { get; set; }
    }

    public struct Attractions
    {
        public string Name;
        public string Description;
        public string CoordinateAt;

        public Attractions(string n, string d, string c)
        {
            Name = n;
            Description = d;
            CoordinateAt = c;
        }

    }

}