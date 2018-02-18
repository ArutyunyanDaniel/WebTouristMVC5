namespace WebTourist.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attraction")]
    public partial class Attraction
    {
        public int id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DbGeography Coordinate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CoordinateOGC { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }
    }
}
