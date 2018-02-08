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


        public virtual DbSet<SaveTable> SaveTable { get; set; }

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


        public string FindNearestWay(string userLocation)
        {
            PointLatLng userLoc = Helper.StringPointToPointLatLng(userLocation);
            PointLatLng nearestPoint = new PointLatLng();
            double maxDistance = Double.MaxValue;

            using (DbContextTourist dbContext = new DbContextTourist())
            {
                CleareTableVisitedRoutes(dbContext);
                List<Route> routes = dbContext.Routes.ToList();
                int IdVisitedExcursionRout = 0;
                foreach (var item in routes)
                {
                    List<PointLatLng> pointsStartedRoute = Helper.StringToListLatLng(item.CoordinatesStartingPointsRouteOGC);
                    foreach (var point in pointsStartedRoute)
                    {
                        double distance = Map.GetRouteDistance(userLoc, point);
                        if (distance < maxDistance)
                        {
                            maxDistance = distance;
                            nearestPoint = point;
                            IdVisitedExcursionRout = item.ID;
                        }
                    }
                }
                AddIdRouteToSaveTable(dbContext, IdVisitedExcursionRout);
            }
            return Helper.ListLatLngToString(Map.GetRoute(userLoc, nearestPoint));
        }

        public string GetNextRoute(string userLocation)
        {
            PointLatLng userLoc = Helper.StringPointToPointLatLng(userLocation);
            PointLatLng nearestPoint = new PointLatLng();
            double maxDistance = Double.MaxValue;
            
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                List<Route> routes = dbContext.Routes.ToList();
                List<SaveTable> listVisitedExcursionRoute = dbContext.SaveTable.ToList();

                int IdVisitedExcursionRout = 0;
                foreach (var item in routes)
                {
                    if (listVisitedExcursionRoute.Count() == dbContext.Routes.Count())
                    { 
                        CleareTableVisitedRoutes(dbContext);
                        listVisitedExcursionRoute = dbContext.SaveTable.ToList();
                    }

                    if (!isVisited(item.ID, listVisitedExcursionRoute))
                    {
                        List<PointLatLng> pointsStartedRoute = Helper.StringToListLatLng(item.CoordinatesStartingPointsRouteOGC);
                        foreach (var point in pointsStartedRoute)
                        {
                            double distance = Map.GetRouteDistance(userLoc, point);
                            if (distance < maxDistance)
                            {
                                maxDistance = distance;
                                nearestPoint = point;
                                IdVisitedExcursionRout = item.ID;
                            }
                        }
                    }
                }
                AddIdRouteToSaveTable(dbContext, IdVisitedExcursionRout);
            }
            return Helper.ListLatLngToString(Map.GetRoute(userLoc, nearestPoint));
        }

        private void CleareTableVisitedRoutes(DbContextTourist db)
        {
            db.SaveTable.RemoveRange(db.SaveTable);
            db.SaveChanges();
        }

        private void AddIdRouteToSaveTable(DbContextTourist db, int id)
        {
            SaveTable sT = new SaveTable();
            sT.IddVisitedExcursionRoute = id;
            db.SaveTable.Add(sT);
            db.SaveChanges();
        }
        private bool isVisited(int id, List<SaveTable> listVisited)
        { 
            foreach (var item in listVisited)
            {
                if (item.IddVisitedExcursionRoute == id)
                    return true;
            }
            return false;
        }
    }
}
