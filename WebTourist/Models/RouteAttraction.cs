namespace WebTourist.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RouteAttraction")]
    public partial class RouteAttraction
    {
        public int Id { get; set; }

        public int RouteID { get; set; }

        public int AttractionID { get; set; }

        public virtual Attraction Attraction { get; set; }

        public virtual Route Route { get; set; }
    }
}
