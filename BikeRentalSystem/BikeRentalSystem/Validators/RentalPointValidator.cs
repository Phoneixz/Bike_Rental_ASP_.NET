using BikeRentalSystem.Models;
using FluentValidation;

namespace BikeRentalSystem.Validators
{
    public class RentalPointValidator : AbstractValidator<RentalPoint>
    {
        public RentalPointValidator()
        {
            RuleFor(rp => rp.Name)
                .NotNull().WithMessage("Name is required.")
                .Length(5, 50).WithMessage("Name should be between 5 and 50 characters.");
            RuleFor(rp => rp.Address)
                .NotNull().WithMessage("Address is required.")
                .Length(5, 100).WithMessage("Address should be between 5 and 100 characters.");
        }
    }
}