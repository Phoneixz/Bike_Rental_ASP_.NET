namespace BikeRentalSystem.ViewModels;
using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class VehicleDetailViewModel
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImgURL { get; set; }
    public int VehicleTypeId { get; set; }
    public VehicleType VehicleType { get; set; }
    public bool Availability { get; set; }
    public IEnumerable<SelectListItem> VehicleTypes { get; set; }
}
