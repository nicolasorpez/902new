using System.ComponentModel.DataAnnotations;

namespace IDGS902UT.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150, ErrorMessage = "La description debe ser maximo de 150 caracteres")]
        public string? Description { get; set; }
    }
}
