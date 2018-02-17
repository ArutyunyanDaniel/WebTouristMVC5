using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMap.NET;

namespace WebTourist.Models
{
    public class RouteInformation
    {
        public RouteInformation()
        {
            listIdVisitedRoutes = new List<int>();
        }

        public double startCoordinatesLat { get; set; }
        public double startCoordinatesLng { get; set; }

        public double finishCoordinatesLat { get; set; }
        public double finishCoordinatesLng { get; set; }

        public string Distance { get; set; }
        public string Duration { get; set; }

        public List<int> listIdVisitedRoutes { get; set; }
        public string WayToExcursionRoute { get; set; }
        public string ExcursionRoute { get; set; }

        public void SetInformationAboutRoute(GDirections diraction,int idVisitedRoute)
        {
            startCoordinatesLat = diraction.StartLocation.Lat;
            startCoordinatesLng = diraction.StartLocation.Lng;

            finishCoordinatesLat = diraction.EndLocation.Lat;
            finishCoordinatesLng = diraction.EndLocation.Lng;

            Distance = diraction.Distance;
            Duration = diraction.Duration;

            listIdVisitedRoutes.Add(idVisitedRoute);
            WayToExcursionRoute = Helper.ListLatLngToString(diraction.Route);
        }

    }
}