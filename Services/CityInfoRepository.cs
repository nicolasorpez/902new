using IDGS902UT.API.DbContexts;
using IDGS902UT.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace IDGS902UT.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext _dbContext;

        public CityInfoRepository(CityInfoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> ExistCityAsync(int id)
        {
            return await _dbContext.Cities.AnyAsync(c => c.Id.Equals(id));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _dbContext.Cities.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByNameAsync(string nombre)
        {
            return await _dbContext.Cities.Where(c => c.Name.Contains(nombre)).ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int id, bool incluidepois)
        {

            if (incluidepois) 
            {
                return await _dbContext.Cities.Include(c=> c.PointsOfInterest).FirstOrDefaultAsync(c => c.Id.Equals(id));
            }

            return await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public async Task<PointOfInterest> GetPointOfInterestAsync(int cityid, int poi)
        {
            return await _dbContext.PoinstOfInterest.FirstOrDefaultAsync(p => p.Id.Equals(poi) & p.CityId.Equals(cityid));
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestsAsync(int cityid)
        {
            return await _dbContext.PoinstOfInterest.Where(p=> p.CityId.Equals(cityid)).ToListAsync();
        }
    }
}
