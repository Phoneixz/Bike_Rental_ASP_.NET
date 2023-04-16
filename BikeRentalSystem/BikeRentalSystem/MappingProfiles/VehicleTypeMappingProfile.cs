using AutoMapper;
using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.MappingProfiles
{
    public class VehicleTypeMappingProfile : Profile
    {
        public VehicleTypeMappingProfile() 
        {
            CreateMap<VehicleType, SelectListItem>()
                .ForMember(d => d.Value, opt => opt.MapFrom(src => src.Id.ToString())).
                ForMember(d => d.Text, opt => opt.MapFrom(src => src.Type));
        }
    }
}
