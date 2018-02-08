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
        public string Coordinate;

        public Attractions(string name, string description, string coordinate)
        {
            Name = name;
            Description = description;
            Coordinate = coordinate;
        }

    }

}