using IDGS902UT.API.Entities;

namespace IDGS902UT.API.Services
{
    public interface ICityInfoRepository
    {
        //obtiene las ciudades
        Task<IEnumerable<City>> GetCitiesAsync();
        
        //get ciudades por nombre o parte de el
        Task<IEnumerable<City>> GetCitiesByNameAsync(string nombre);

        //obtiene una ciudad por id
        Task<City> GetCityByIdAsync(int id, bool incluidepois);

        Task<bool> ExistCityAsync(int id);





        //obtener los puntos de interes de una ciudad!
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsAsync(int cityid);

        //obtener 1 punto de interes de una determinada ciudad
        Task<PointOfInterest> GetPointOfInterestAsync(int cityid,int poi);



    }
}
