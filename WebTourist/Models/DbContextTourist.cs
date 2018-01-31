namespace WebTourist.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
    }
}
