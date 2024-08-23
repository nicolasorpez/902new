using IDGS902UT.API.Models;

namespace IDGS902UT_API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new()
            {
                new CityDto
                {
                  Id = 1,
                  Name = "Iguala de la Independencia",
                  Description = "La bella calurosa!!!",
                  PointsOfInterest =
                  {
                    new PointOfInterestDto{Id = 1, Name = "Asta Monumental", Description = "La más alta de México"},
                    new PointOfInterestDto{Id = 2, Name = "Laguna de Tuxpan", Description = "Pa' la cruda"},
                    new PointOfInterestDto{Id = 3, Name = "Centro Hitórico", Description = "Cuna de la Bandera Naciona"}
                  }
                },
                new CityDto
                {
                  Id = 2,
                  Name = "Taxco de alarcón",
                  Description = "Ciudad colonial",
                  PointsOfInterest =
                  {
                    new PointOfInterestDto{Id = 4, Name = "Catedral de Santa Prisca", Description = "Arquitectura gótica"},
                    new PointOfInterestDto{Id = 5, Name = "Cristo", Description = "Limpia tus pecados"}
                  }
                },
                new CityDto
                {
                  Id = 3,
                  Name = "Acapulco de Juarez",
                  Description= "La bahía más hermosa de México",
                  PointsOfInterest =
                  {
                    new PointOfInterestDto{Id = 6, Name = "La Quebrada", Description = "Clavados en Mar Abierto"},
                    new PointOfInterestDto{Id = 7, Name = "La Diana Cazadora", Description = "Fuente"},
                    new PointOfInterestDto{Id = 8, Name = "Parque Papagayo", Description = "Se lo llevo el Huracan"}
                  }
                }
            };
        }//
    }
}