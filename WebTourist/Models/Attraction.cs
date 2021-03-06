namespace WebTourist.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Script.Serialization;

    [Table("Attraction")]
    public partial class Attraction
    {
        public Attraction() { }
        public Attraction(string name, string description, string coordinate)
        {
            Name = name;
            Description = description;
            CoordinateOGC = coordinate;
        }

        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ScriptIgnore]
        public DbGeography Coordinate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CoordinateOGC { get; set; }

        [ScriptIgnore]
        public int CityID { get; set; }
        [ScriptIgnore]
        public virtual City City { get; set; }
    }
}
