using AutoMapper;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.MappingProfiles
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<Reservation, ReservationDetailViewModel>();
        }

    }
}
