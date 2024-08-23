using AutoMapper;
using IDGS902UT.API.Models;
using IDGS902UT.API.Services;
using IDGS902UT_API;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace IDGS902UT.API.Controllers
{
    [ApiController]

    [Route ("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _repository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var ciudades = await _repository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CitySinPois>>(ciudades));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id, bool includepois = false)
        {
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id.Equals (id));

            // if (city == null) 
            //     {
            //         return NotFound($"La ciudad con el id {id} no existe en la BD");
            //     }

            // return Ok(city);

            if (!await _repository.ExistCityAsync(id))

            {
                return NotFound($"La ciudad con el id{id} no existe en la BD");
            }

            var city = await _repository.GetCityByIdAsync(id, includepois);

            if (includepois)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CitySinPois>(city));

        }

        [HttpGet("{id}/name")]
        public IActionResult GetCityByName(int name)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id.Equals(name));

            if (city == null)
            {
                return NotFound($"La ciudad con el id {name} no existe en la BD");
            }

            return Ok(city.Name);
        }
        





        //[HttpGet("api/PersonalData")]
        //public JsonResult GetPersonalData()
        //{
        //    return new JsonResult(new List<object>
        //    {
        //        new { id = 1, Nombre = "Javier", Correo = "angel@gmail.com", Telefono = "733"},

        //    });
        //}
    }
}
