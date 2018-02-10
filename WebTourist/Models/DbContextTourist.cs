namespace WebTourist.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using GMap.NET;

    public partial class DbContextTourist : DbContext
    {
        public DbContextTourist()
            : base("name=DbContextTourist")
        {
        }

        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteAttraction> RouteAttractions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.RouteAttractions)
                .WithRequired(e => e.Attraction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Route>()
                .HasMany(e => e.RouteAttractions)
                .WithRequired(e => e.Route)
                .WillCascadeOnDelete(false);
        }

        public List<ContainerRouteAttractions> GetAttractionAndExcursionRoutes()
        {
            List<ContainerRouteAttractions> result = new List<ContainerRouteAttractions>();
            List<RouteAttraction> listRouteAttractions;
            List<Attraction> ls;
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                listRouteAttractions = dbContext.RouteAttractions.ToList();
                ls = dbContext.Attractions.ToList();

                ContainerRouteAttractions tempRA = new ContainerRouteAttractions();
                foreach (var item in listRouteAttractions)
                {
                    if (tempRA.IdR == 0)
                    {
                        tempRA.IdR = item.Route.ID;
                        tempRA.CoordinatesRoute = Helper.DeleteLetterFromString(item.Route.CoordinatesOGC);

                    }
                    if (tempRA.IdR == item.Route.ID)
                    {
                        Attractions at = new Attractions(item.Attraction.Name, item.Attraction.Description, Helper.DeleteLetterFromString(item.Attraction.CoordinateOGC));
                        tempRA.Attractions.Add(at);
                    }
                    else
                    {
                        result.Add(tempRA);
                        tempRA = new ContainerRouteAttractions();
                        Attractions at = new Attractions(item.Attraction.Name, item.Attraction.Description, Helper.DeleteLetterFromString(item.Attraction.CoordinateOGC));
                        tempRA.Attractions.Add(at);
                    }
                }
                result.Add(tempRA);
            }
            return result;
        }


        public RouteInformation FindNearestWay(RouteInformation routeInformation)
        {
            PointLatLng userLoc = new PointLatLng(routeInformation.startCoordinatesLat, routeInformation.startCoordinatesLng);
            PointLatLng nearestPoint = new PointLatLng();
            double maxDistance = Double.MaxValue;
            int IdVisitedExcursionRout = 0;

            using (DbContextTourist dbContext = new DbContextTourist())
            {
                List<Route> routes = dbContext.Routes.ToList();
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
                            IdVisitedExcursionRout = item.ID;
                        }
                    }
                }
            }
            return PrepareRouteInformation(routeInformation, userLoc, nearestPoint, IdVisitedExcursionRout);
        }

        public RouteInformation GetNextRoute(RouteInformation routeInformation)
        {
            PointLatLng userLoc = new PointLatLng(routeInformation.startCoordinatesLat, routeInformation.startCoordinatesLng);
            PointLatLng nearestPoint = new PointLatLng();
            double maxDistance = Double.MaxValue;
            int IdVisitedExcursionRout = 0;
            int countExcurisonRoutes = 0;
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                List<Route> routes = dbContext.Routes.ToList();
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
                                IdVisitedExcursionRout = item.ID;
                            }
                        }
                    }
                }
            }

            return PrepareRouteInformation(routeInformation, userLoc, nearestPoint,
                IdVisitedExcursionRout, countExcurisonRoutes);
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
            PointLatLng finish, int idVisitedRoute, int countExcurisonRoutes = -1)
        {
            var diraction = Map.GetDiraction(start, finish);
            routeInformation.SetInformationAboutRoute(diraction);
            routeInformation.listIdVisitedRoutes.Add(idVisitedRoute);

            if (routeInformation.listIdVisitedRoutes.Count == countExcurisonRoutes)
                routeInformation.listIdVisitedRoutes.Clear();
            
            return routeInformation;
        }
    }
}
