namespace WebTourist.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;
    using GMap.NET;
    using System.Web.Mvc;

    public partial class DbContextTourist : DbContext
    {
        public DbContextTourist()
            : base("name=DbContextTourist1")
        {
        }

        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Attractions)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Routes)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);
        }


        public int GetIdCurrentCity(string selectedCity)
        {
            int idCurrentCity;
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                bool temp = dbContext.Cities.Any(u => u.Name == selectedCity);
                if (temp)
                    idCurrentCity = dbContext.Cities.Where(a => a.Name == selectedCity).Select(w => w.Id).ToList()[0];
                else
                    idCurrentCity = -1;
            }
            return idCurrentCity;
        }

        public List<AttractionsInformations> GetAttractions(int idCurrentCity)
        {
            List<AttractionsInformations> attractions = new List<AttractionsInformations>();
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                var temp = dbContext.Attractions.Where(a=>a.CityID==idCurrentCity).ToList();
                foreach (var item in temp)
                {
                    attractions.Add(new AttractionsInformations(item.Name, item.Description, Helper.DeleteLetterFromString(item.CoordinateOGC)));
                }
            }
            return attractions;
        }

        public List<string> GetExcursionRoutes(int idCurrentCity)
        {
            List<string> excursionRoutes = new List<string>();
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                var temp = dbContext.Routes.Where(a=>a.CityID == idCurrentCity).ToList();
                foreach (var item in temp)
                {
                    excursionRoutes.Add(Helper.DeleteLetterFromString(item.CoordinatesOGC));
                }
            }
            return excursionRoutes;
        }

        public RouteInformation FindNearestWay(RouteInformation routeInformation)
        {
            PointLatLng userLoc = new PointLatLng(routeInformation.startCoordinatesLat, routeInformation.startCoordinatesLng);
            PointLatLng nearestPoint = new PointLatLng();
            string excursionRoute = String.Empty;
            double maxDistance = Double.MaxValue;
            int IdVisitedExcursionRout = 0;

            using (DbContextTourist dbContext = new DbContextTourist())
            {  
                List<Route> routes = dbContext.Routes.Where(a => a.CityID == routeInformation.IdCurrentCity).ToList();

                foreach (var item in routes)
                {
                    List<PointLatLng> pointsStartedRoute = Helper.StringToListLatLng(item.CoordinatesStartingPointsRouteOGC);
                    foreach (var pointSR in pointsStartedRoute)
                    {
                        double distance = Map.GetRouteDistance(userLoc, pointSR);
                        if (distance < maxDistance)
                        {
                            maxDistance = distance;
                            nearestPoint = pointSR;
                            excursionRoute = Helper.DeleteLetterFromString(item.CoordinatesOGC);
                            IdVisitedExcursionRout = item.ID;
                        }
                    }
                }
            }
            return PrepareRouteInformation(routeInformation, userLoc, nearestPoint, IdVisitedExcursionRout, excursionRoute);
        }

        public RouteInformation GetNextRoute(RouteInformation routeInformation)
        {
            PointLatLng userLoc = new PointLatLng(routeInformation.startCoordinatesLat, routeInformation.startCoordinatesLng);
            PointLatLng nearestPoint = new PointLatLng();
            string excursionRoute = String.Empty;
            double maxDistance = Double.MaxValue;
            int IdVisitedExcursionRout = 0;
            int countExcurisonRoutes = 0;
            using (DbContextTourist dbContext = new DbContextTourist())
            {        
                List<Route> routes = dbContext.Routes.Where(a => a.CityID == routeInformation.IdCurrentCity).ToList();
                countExcurisonRoutes = routes.Count();

                foreach (var item in routes)
                {
                    if (!isVisited(item.ID, routeInformation.listIdVisitedRoutes))
                    {
                        List<PointLatLng> pointsStartedRoute = Helper.StringToListLatLng(item.CoordinatesStartingPointsRouteOGC);
                        foreach (var points in pointsStartedRoute)
                        {
                            double distance = Map.GetRouteDistance(userLoc, points);
                            if (distance < maxDistance)
                            {
                                maxDistance = distance;
                                nearestPoint = points;
                                excursionRoute = Helper.DeleteLetterFromString(item.CoordinatesOGC);
                                IdVisitedExcursionRout = item.ID;
                            }
                        }
                    }
                }
            }

            return PrepareRouteInformation(routeInformation, userLoc, nearestPoint,
                IdVisitedExcursionRout, excursionRoute, countExcurisonRoutes);
        }

        private bool isVisited(int id, List<int> listVisited)
        {
            foreach (var item in listVisited)
            {
                if (item == id)
                    return true;
            }
            return false;
        }

        private RouteInformation PrepareRouteInformation(RouteInformation routeInformation, PointLatLng start,
            PointLatLng finish, int idVisitedRoute, string excrustionRoute, int countExcurisonRoutes = -1)
        {
            var diraction = Map.GetDiraction(start, finish);
            routeInformation.SetInformationAboutRoute(diraction, idVisitedRoute);
            routeInformation.ExcursionRoute = excrustionRoute;

            if (routeInformation.listIdVisitedRoutes.Count == countExcurisonRoutes)
                routeInformation.listIdVisitedRoutes.Clear();

            return routeInformation;
        }
    }
}
