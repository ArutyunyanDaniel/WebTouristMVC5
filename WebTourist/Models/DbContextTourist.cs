namespace WebTourist.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

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
            using (DbContextTourist dbContext = new DbContextTourist())
            {
                var List = dbContext.RouteAttractions.ToList();
                var ls = dbContext.Attractions.ToList();
                var ll = dbContext.Attractions.ToList();
                ContainerRouteAttractions routeAttraction = new ContainerRouteAttractions();
                foreach (var item in List)
                {
                    if (routeAttraction.IdR == 0)
                    {
                        routeAttraction.IdR = item.Route.ID;
                        routeAttraction.CoordinatesRoute = Helper.DEleteleterFS(item.Route.CoordinatesOGC);
                       
                    }
                    if (routeAttraction.IdR == item.Route.ID)
                    {
                        Attractions at = new Attractions(item.Attraction.Name, item.Attraction.Description, Helper.DeleteLetterFromString(item.Attraction.CoordinateOGC));
                        routeAttraction.Attractions.Add(at);
                    }
                    else
                    {
                        result.Add(routeAttraction);
                        routeAttraction = new ContainerRouteAttractions();
                        Attractions at = new Attractions(item.Attraction.Name, item.Attraction.Description, Helper.DeleteLetterFromString(item.Attraction.CoordinateOGC));
                        routeAttraction.Attractions.Add(at);
                    }
                }
                result.Add(routeAttraction);
            }
            return result;
        }
    }
}
