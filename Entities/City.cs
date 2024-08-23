using System.ComponentModel.DataAnnotations;

namespace IDGS902UT.API.Entities
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }

        // una ciudad puede tener varios puntos de interes

        public List<PointOfInterest>PointsOfInterest { get; set; } = new();
    }
}
