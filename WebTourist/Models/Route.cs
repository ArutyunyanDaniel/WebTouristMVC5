namespace WebTourist.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using GMap.NET;

    [Table("Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            RouteAttractions = new HashSet<RouteAttraction>();
        }

        public int ID { get; set; }

        [Required]
        public DbGeography Coordinates { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CoordinatesOGC { get; set; }

        [Required]
        public DbGeography CoordinatesStartingPointsRoute { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CoordinatesStartingPointsRouteOGC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteAttraction> RouteAttractions { get; set; }

        public string FindNearestWay(string userLocation, List<Route> routest)
        {
            PointLatLng userLoc = Helper.StringPointToPointLatLng(userLocation);
            PointLatLng nearestPoint = new PointLatLng();
            double maxDistance = Double.MaxValue;
            foreach (var item in routest)
            {
                List<PointLatLng> pointsStartedRoute = Helper.StringToListLatLng(item.CoordinatesStartingPointsRouteOGC);
                foreach (var point in pointsStartedRoute)
                {
                    double distance = Map.GetRouteDistance(userLoc, point);
                    if (distance < maxDistance)
                    {
                        maxDistance = distance;
                        nearestPoint = point;
                    }
                }
            }
            return Helper.ListLatLngToString(Map.GetRoute(userLoc, nearestPoint));
        } 
    }
}
