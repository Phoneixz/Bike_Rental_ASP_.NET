using AutoMapper;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;

namespace BikeRentalSystem.MappingProfiles
{
    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            CreateMap<Vehicle, VehicleDetailViewModel>();
        }
    }
}
