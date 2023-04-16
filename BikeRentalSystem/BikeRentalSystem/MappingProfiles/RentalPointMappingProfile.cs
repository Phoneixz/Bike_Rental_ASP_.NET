using AutoMapper;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.MappingProfiles
{
    public class RentalPointMappingProfile : Profile
    {
        public RentalPointMappingProfile()
        {
            CreateMap<RentalPoint, RentalPointDetailViewModel>();
            CreateMap<RentalPoint, SelectListItem>()
                .ForMember(d => d.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}
