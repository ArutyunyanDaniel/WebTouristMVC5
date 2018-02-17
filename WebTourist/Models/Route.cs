namespace WebTourist.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        public int ID { get; set; }

        [Required]
        public DbGeography Coordinates { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CoordinatesOGC { get; set; }

        [Required]
        public DbGeography CoordinatesStartingPointsRoute { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CoordinatesStartingPointsRouteOGC { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }
    }
}
