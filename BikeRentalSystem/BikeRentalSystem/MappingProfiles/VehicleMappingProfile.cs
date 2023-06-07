using AutoMapper;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.MappingProfiles
{
    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            CreateMap<Vehicle, VehicleDetailViewModel>();
            CreateMap<Vehicle, SelectListItem>()
                        .ForMember(d => d.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                        .ForMember(d => d.Text, opt => opt.MapFrom(src => $"{src.Make} - {src.VehicleType.Type} - {src.Price}"));
        }
    }
}
