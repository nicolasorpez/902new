using AutoMapper;
using IDGS902UT.API.Models;
using IDGS902UT.API.Services;
using IDGS902UT_API;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace IDGS902UT.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityid}/pointsofinterest")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ICityInfoRepository _repository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ICityInfoRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult>GetPointsofInterestByCityId(int cityid)
        {

            if (! await _repository.ExistCityAsync(cityid))
            {
                return NotFound("No existe la ciudad en la BD");
            }

            var pois = await _repository.GetPointsOfInterestsAsync(cityid);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pois));

        }

        [HttpGet("{poid}", Name = "GetPointOfInterest")]
        public IActionResult GetPuntodeInteresById(int cityid, int poid)
        {
            var city = CitiesDataStore.Current.Cities?.FirstOrDefault(c => c.Id.Equals(cityid));
            if (city == null)
            {
                return NotFound($"No existe ninguna ciudad con el id {cityid} en la BD");
            }

            else
            {
                var point = city.PointsOfInterest.FirstOrDefault(p => p.Id.Equals(poid));
                if (point == null)
                {
                    return NotFound($"No existe ningun punto de interes con el id {poid}, en {city.Name}");
                }
                return Ok(point);
            }
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityid, [FromBody] PointOfInterestForCreationDto nuevopoi)

        {    //buscar la ciudad y si no se encuentra mandar un error 404
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id.Equals(cityid));
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                int maxpoiId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);
                //crear un punto de interes Dto
                var finalpoi = new PointOfInterestDto
                {
                    Id = ++maxpoiId,
                    Name = nuevopoi.Name,
                    Description = nuevopoi.Description
                };
                //agregarlo al almacen de datos de la ciudad
                city.PointsOfInterest.Add(finalpoi);
                //regresar el status Code adecuado y la URL al nuevo recurso
                return CreatedAtRoute("GetPointOfInterest", new
                {
                    cityid = cityid,
                    poid = finalpoi.Id,
                }, finalpoi);

            }


        }

        [HttpPut("{poid}")]
        public IActionResult UpdatePointOfInterest(int cityid, int poid, [FromBody] PointOfInterestForUpdateDto updatepoi)
        {
            var city = CitiesDataStore.Current.Cities?.FirstOrDefault(c => c.Id.Equals(cityid));
            if (city == null)
            {
                return NotFound($"No existe ninguna ciudad con el id {cityid} en la BD");
            }

            else
            {
                var point = city.PointsOfInterest.FirstOrDefault(p => p.Id.Equals(poid));
                if (point == null)
                {
                    return NotFound($"No existe ningun punto de interes con el id {poid}, en {city.Name}");
                }
                else
                {
                    point.Name = updatepoi.Name;
                    point.Description = updatepoi.Description;

                    return NoContent();
                }

            }
        }

        [HttpDelete("{poid}")]
        public IActionResult DeletePointOfInterest(int cityid, int poid)
        {
            var city = CitiesDataStore.Current.Cities?.FirstOrDefault(c => c.Id.Equals(cityid));
            if (city == null)
            {
                return NotFound($"No existe ninguna ciudad con el id {cityid} en la BD");
            }

            else
            {
                var point = city.PointsOfInterest.FirstOrDefault(p => p.Id.Equals(poid));
                if (point == null)
                {
                    return NotFound($"No existe ningun punto de interes con el id {poid}, en {city.Name}");
                }
                city.PointsOfInterest.Remove(point);
                return NoContent();

            }

        }

        [HttpPatch("{poid}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityid, int poid, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            //Validar la ciudad
            var city = CitiesDataStore.Current.Cities?.FirstOrDefault(c => c.Id.Equals(cityid));
            if (city is null)
            {
                return NotFound($"No se encontró la ciudad con el id {cityid}");
            }
            //Validar punto de interes
            var pointFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id.Equals(poid));
            if (pointFromStore is null)
            {
                return NotFound($"No se encontró el punto de interés especificado con el id {poid} en: {city.Name}");
            }

            //transformar el tipo de dato del store
            var poiToPatch = new PointOfInterestForUpdateDto
            {
                Name = pointFromStore.Name,
                Description = pointFromStore.Description
            };

            //Aplicar cambios del PatchDocument
            patchDocument.ApplyTo(poiToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(poiToPatch))
            {
                return BadRequest();
            }

            //si los cambios son válidos dentro del patch, entonces se aplican al objeto dela Store

            pointFromStore.Name = poiToPatch.Name;
            pointFromStore.Description = poiToPatch.Description;

            return NoContent(); //Se actualizan los datos y regresa 204 No Content por la actualización parcial
        }




    }
        

}
