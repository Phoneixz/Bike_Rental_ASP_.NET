using BikeRentalSystem.Models;
using FluentValidation;

namespace BikeRentalSystem.Validators
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(v => v.Make)
                .NotNull().WithMessage("Make is required.")
                .Length(5, 50).WithMessage("Make should be between 5 and 50 characters.");
            RuleFor(v => v.Description)
                .NotNull().WithMessage("Description is required.")
                .Length(5, 200).WithMessage("Description should be between 5 and 200 characters.");
            RuleFor(v => v.Price)
                .GreaterThan(0).WithMessage("Price should be greater than 0.");
            RuleFor(v => v.ImgURL)
                .NotNull().WithMessage("Image URL is required.")
                .Length(5, 200).WithMessage("Image URL should be between 5 and 200 characters.");
            RuleFor(v => v.VehicleType)
                .NotNull().WithMessage("Vehicle type is required.");
            RuleFor(v => v.RentalPoint)
                .NotNull().WithMessage("Rental point is required.");
        }
    }
}