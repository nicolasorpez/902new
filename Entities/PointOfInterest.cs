using System.ComponentModel.DataAnnotations;

namespace IDGS902UT.API.Entities
{
    public class PointOfInterest
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }


        //un punto de interest pertenece a una ciudad
        //crear un FK
        public int CityId { get; set; }
        //crear el prop de navegacion
        public City City { get; set; }
    }
}