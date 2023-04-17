using BikeRentalSystem.Models;
using FluentValidation;

namespace BikeRentalSystem.Validators
{
    public class VehicleTypeValidator : AbstractValidator<VehicleType>
    {
        public VehicleTypeValidator()
        {
            RuleFor(vt => vt.Type)
                .NotNull().WithMessage("Type is required.")
                .Length(5, 50).WithMessage("Type should be between 5 and 50 characters.");
        }
    }
}