using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTourist.Models
{
    [Serializable]
    public class Point
    {
        public Point()
        {
            listIdVisitedRoutes = new List<int>();
        }
        public double coordinateLat { get; set; }
        public double coordinateLng { get; set; }
        public List<int> listIdVisitedRoutes { get; set; }
        public string pathToExcursionRoute { get; set; }

    }
}