using AutoMapper;
using IDGS902UT.API.Entities;
using IDGS902UT.API.Models;

namespace IDGS902UT.API.Perfiles
{
    public class Profiles : Profile
    {
        public Profiles() 
        {
            //perfiles para cities
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap <City, CitySinPois>().ReverseMap();


            //perfiles para points of interest
            CreateMap<PointOfInterest, PointOfInterestDto>().ReverseMap();




        }
    }
}
